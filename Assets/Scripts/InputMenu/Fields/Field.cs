using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Field : MonoBehaviour {
    public GameObject input;

    public abstract object getValue ();
    public abstract void setOnValidate (UnityAction action);
}
