using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class IntField : StringField {
	public override object getValue () {
		return int.Parse((string)base.getValue());
	}
}
