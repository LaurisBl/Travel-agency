using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArrayField : Field {
	[SerializeField] private TMP_Text NameLabel;
	private Transform ItemList => input.transform;

	private ItemPrototype prototype = new();

	private List<Dictionary<string, MenuField>> fields = new();

	private UnityEvent onValidate = new();

	public ArrayField init(string key) {
		NameLabel.text = key;

		return this;
	}

	public override object getValue () {
		var values = new List<Dictionary<string, object>>();

		foreach(var dict in fields) {
			var item = new Dictionary<string, object>();

			foreach(var (key, value) in dict) {
				item.Add(key, value.correspondingField.getValue());
			}

			values.Add(item);
		}

		return values;
	}

	public override void setOnValidate (UnityAction action) {
		onValidate.AddListener(action);
	}

	public void Create() {
		var item = new Dictionary<string, MenuField>();

		int i = fields.Count;
		var itemModule = Instantiate(MainManager.instance.itemPrototypePrefab, ItemList);
		itemModule.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Elementas " + i;
		itemModule.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { DestroyItem(i); });
		var list = itemModule.GetChild(1);

		foreach (var (key, value) in prototype.fields) {
			switch (value) {
				case 0: // Int
					var module = createModule(key, list);

					Field field = Instantiate(MainManager.instance.intFieldPrefab, module);
					field.setOnValidate(onValidate.Invoke);

					item.Add(key, new MenuField() { correspondingField = field });
					break;

				case 1: // Float
					module = createModule(key, list);

					field = Instantiate(MainManager.instance.floatFieldPrefab, module);

					item.Add(key, new MenuField() { correspondingField = field });
					field.setOnValidate(onValidate.Invoke);
					break;

				case 2: // String
					module = createModule(key, list);

					field = Instantiate(MainManager.instance.stringFieldPrefab, module);

					item.Add(key, new MenuField() { correspondingField = field });
					field.setOnValidate(onValidate.Invoke);
					break;

				case 3: // Email
					module = createModule(key, list);

					field = Instantiate(MainManager.instance.emailFieldPrefab, module);

					item.Add(key, new MenuField() { correspondingField = field });
					field.setOnValidate(onValidate.Invoke);
					break;

				case 4: // Dropdown
					module = createModule(key, list);

					field = Instantiate(MainManager.instance.dropdownFieldPrefab, module);

					(field as DropdownField).setupOptions(prototype.dropdownOptions[key]);

					item.Add(key, new MenuField() { correspondingField = field });
					field.setOnValidate(onValidate.Invoke);
					break;
			}
		}

		fields.Add(item);
	}

	public void DestroyItem(int index) {
		int children = ItemList.childCount - 1;

		Destroy(ItemList.GetChild(index).gameObject);

		fields.RemoveAt(index);

		index = -1;

		for(int i = 0; i < children; i++) {
			ItemList.GetChild(i).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Item " + i;

			var button = ItemList.GetChild(i).GetChild(2).GetComponent<Button>();

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(delegate { DestroyItem(++index); });
		}
			
	}

	public ArrayField AddIntField (string key) {
		prototype.AddIntField(key);

		return this;
	}

	public ArrayField AddFloatField (string key) {
		prototype.AddFloatField(key);

		return this;
	}

	public ArrayField AddStringField (string key) {
		prototype.AddStringField(key);

		return this;
	}

	public ArrayField AddEmailField (string key) {
		prototype.AddEmailField(key);

		return this;
	}

	public ArrayField AddDropdownField (string key, List<string> options) {
		prototype.AddDropdownField(key, options);

		return this;
	}

	private Transform createModule (string key, Transform list) {
		var module = Instantiate(MainManager.instance.modulePrefab, list);

		module.GetChild(0).GetComponent<TMP_Text>().text = key;

		return module;
	}
}

[System.Serializable]
public class ItemPrototype {
	public List<KeyValuePair<string, int>> fields { get; private set; } = new();
	public Dictionary<string, List<string>> dropdownOptions { get; private set; } = new();

	public void AddIntField (string key) {
		fields.Add(new(key, 0));
	}

	public void AddFloatField (string key) {
		fields.Add(new(key, 1));
	}

	public void AddStringField (string key) {
		fields.Add(new(key, 2));
	}

	public void AddEmailField (string key) {
		fields.Add(new(key, 3));
	}

	public void AddDropdownField (string key, List<string> options) {
		fields.Add(new(key, 4));
		dropdownOptions.Add(key, options);
	}
}
