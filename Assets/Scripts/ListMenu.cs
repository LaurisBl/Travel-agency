using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListMenu : MonoBehaviour {
	public RectTransform ItemList;

	public GameObject ItemPrefab;

	public void ClearList() {
		foreach(Transform item in ItemList)
			Destroy(item.gameObject);
	}
}
