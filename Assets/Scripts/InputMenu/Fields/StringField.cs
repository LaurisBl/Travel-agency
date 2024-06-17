using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StringField : Field {
	public override object getValue () {
		return input.GetComponent<TMP_InputField>().text;
	}

	public override void setOnValidate (UnityAction action) {
		input.GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate(string s) { action.Invoke(); });
	}
}
