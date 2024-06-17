using System.Collections;
using System.Collections.Generic;
using UI.Dates;
using UnityEngine;
using UnityEngine.Events;

public class DateField : Field {
	public override object getValue () {
		return input.GetComponent<DatePicker>().SelectedDate.Date;
	}

	public override void setOnValidate (UnityAction action) {
		
	}
}
