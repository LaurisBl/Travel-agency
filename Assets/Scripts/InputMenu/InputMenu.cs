using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Dates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputMenu : MonoBehaviour
{
	[SerializeField] private TMP_Text MenuName;
	[SerializeField] private Transform moduleList;

    private Dictionary<string, MenuField> fields = new();

	private UnityEvent<Dictionary<string, object>> callback = new();
	private UnityEvent<Dictionary<string, MenuField>, Dictionary<string, object>> onValidate = new();

	public InputMenu Initialize (string menuName, UnityAction<Dictionary<string, object>> _callback) {
        MenuName.text = "Sukurti " + menuName;
        
        callback.AddListener(_callback);

        return this;
	}

    public InputMenu AddIntField(string key) {
        var module = createModule(key);

        var field = Instantiate(MainManager.instance.intFieldPrefab, module);
		field.setOnValidate(CallOnValidate);

		fields.Add(key, new MenuField() { correspondingField = field });

        return this;
    }

	public InputMenu AddFloatField (string key) {
		var module = createModule(key);

		var field = Instantiate(MainManager.instance.floatFieldPrefab, module);
		field.setOnValidate(CallOnValidate);

		fields.Add(key, new MenuField() { correspondingField = field });

		return this;
	}

	public InputMenu AddStringField (string key, bool multiline = false) {
		var module = createModule(key);

		var field = Instantiate(MainManager.instance.stringFieldPrefab, module);
		field.setOnValidate(CallOnValidate);

		if (multiline) {
			module.GetChild(0).GetComponent<TMP_Text>().alignment = TextAlignmentOptions.TopLeft;
			field.GetComponent<TMP_InputField>().lineType = TMP_InputField.LineType.MultiLineNewline;
			field.GetComponent<LayoutElement>().preferredHeight = 60 * 3;
		}

		fields.Add(key, new MenuField() { correspondingField = field });

		return this;
	}

	public InputMenu AddEmailField (string key) {
		var module = createModule(key);

		var field = Instantiate(MainManager.instance.emailFieldPrefab, module);
		field.setOnValidate(CallOnValidate);

		fields.Add(key, new MenuField() { correspondingField = field });

		return this;
	}

	public InputMenu AddDropdownField (string key, List<string> options) {
		var module = createModule(key);

		var field = Instantiate(MainManager.instance.dropdownFieldPrefab, module);

		field.setupOptions(options);
		field.setOnValidate(CallOnValidate);

		fields.Add(key, new MenuField() { correspondingField = field });

		return this;
	}
	
	public InputMenu AddArrayField(string key, Action<ArrayField> declaration) {
		var array = Instantiate(MainManager.instance.arrayFieldPrefab, moduleList);

		array.init(key);
		array.setOnValidate(CallOnValidate);

		declaration.Invoke(array);

		fields.Add(key, new MenuField() { correspondingField = array });

		return this;
	}

	public InputMenu AddDateField(string key) {
		var module = createModule(key);

		var field = Instantiate(MainManager.instance.dateFieldPrefab, module);

		field.input.GetComponent<DatePicker>().SelectedDate = DateTime.Now;

		fields.Add(key, new MenuField() { correspondingField = field });

		return this;
	}

	public InputMenu SetOnValidate(UnityAction<Dictionary<string, MenuField>, Dictionary<string, object>> action) {
		onValidate.AddListener(action);

		return this;
	}

	private Transform createModule(string key) {
		var module = Instantiate(MainManager.instance.modulePrefab, moduleList);

        module.GetChild(0).GetComponent<TMP_Text>().text = key;

        return module;
	}

	private void CallOnValidate() {
		var dict = new Dictionary<string, object>();

		if (onValidate.GetPersistentEventCount() <= 0)
			return;

		foreach (var (key, value) in fields) {
			dict.Add(key, value.correspondingField.getValue());
		}

		onValidate.Invoke(fields, dict);
	}

	public void Cancel() {
		Destroy(gameObject);
	}

    public void Confirm() {
        var dict = new Dictionary<string, object>();

        foreach (var (key, value) in fields)
        {
            dict.Add(key, value.correspondingField.getValue());
        }

        callback.Invoke(dict);

		Destroy(gameObject);
    }
}

public class MenuField { 
    public object value => correspondingField.getValue();

    public Field correspondingField;
}
