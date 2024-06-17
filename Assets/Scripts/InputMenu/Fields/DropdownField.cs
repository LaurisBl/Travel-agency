using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DropdownField : Field {
	public void setupOptions(List<string> options) {
		input.GetComponent<TMP_Dropdown>().options.Clear();

		input.GetComponent<TMP_Dropdown>().AddOptions(options);
	}

	public override object getValue () {
		return input.GetComponent<TMP_Dropdown>().value;
	}

	public override void setOnValidate (UnityAction action) {
		input.GetComponent<TMP_Dropdown>().onValueChanged.AddListener(delegate (int n) { action.Invoke(); });
	}
}
