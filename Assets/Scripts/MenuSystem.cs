using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour {
    public List<GameObject> menus = new();

    public int currentMenu { get; private set; } = 0;

    public void loadMenu(int menuID) {
        foreach(var menu in menus) {
            menu.gameObject.SetActive(false);

            if(menu.TryGetComponent(out TripPanel p))
                p.OnDisable();
        }

        menus[menuID].SetActive(true);

		if (menus[menuID].TryGetComponent(out TripPanel panel))
			panel.OnEnable();

		currentMenu = menuID;
    }
}
