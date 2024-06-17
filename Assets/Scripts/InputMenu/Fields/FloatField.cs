using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FloatField : StringField {
	public override object getValue () {
		try {
			return float.Parse((string)base.getValue());
		} catch { 
			return 0; 
		}
	}
}
