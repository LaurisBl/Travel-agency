using GraphQL.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdminPanel : MonoBehaviour
{
	enum options {
		Countries,
		Cities,
		Points,
		TransportRent
	}

	public GameObject commonElementPrefab;

	public List<Transform> lists = new();

	private void Start () {
		updateData();
	}

	private async void updateData () {
		var countries = await MainManager.instance.client.GetCountries();
		var cities = await MainManager.instance.client.GetCities();
		var points = await MainManager.instance.client.GetPoints();
		var rents = await MainManager.instance.client.GetTransportationRentList();

		for (int i = 0; i < lists.Count; i++)
			foreach (Transform child in lists[i])
				Destroy(child.gameObject);

		foreach (var elem in countries) {
			var obj = Instantiate(commonElementPrefab, lists[0]);

			obj.name = elem.CountryID.ToString();

			obj.transform.GetChild(0).GetComponent<TMP_Text>().text = elem.Name;

			obj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(async delegate {
				await MainManager.instance.client.DeleteCountry(elem.CountryID);

				Destroy(obj);
			});
		}

		foreach (var elem in cities) {
			var obj = Instantiate(commonElementPrefab, lists[1]);

			obj.name = elem.CityID.ToString();

			obj.transform.GetChild(0).GetComponent<TMP_Text>().text = elem.Name;

			obj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(async delegate {
				await MainManager.instance.client.DeleteCity(elem.CityID);

				Destroy(obj);
			});
		}

		foreach (var elem in points) {
			var obj = Instantiate(commonElementPrefab, lists[2]);

			obj.name = elem.PointID.ToString();

			obj.transform.GetChild(0).GetComponent<TMP_Text>().text = elem.Name;

			obj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(async delegate {
				await MainManager.instance.client.DeletePoint(elem.PointID);

				await MainManager.instance.client.RemoveImagesForPoint(elem.PointID);

				Destroy(obj);
			});
		}

		foreach (var elem in rents) {
			var obj = Instantiate(commonElementPrefab, lists[3]);

			obj.name = elem.TransportationRentID.ToString();

			obj.transform.GetChild(0).GetComponent<TMP_Text>().text = elem.CompanyName;

			obj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(async delegate {
				await MainManager.instance.client.RemoveTransportationRent(elem.TransportationRentID);

				Destroy(obj);
			});
		}
	}

	public async void AddItem(int list) {
		switch ((options)list) {
			case options.Countries:
				MainManager.instance.CreateInputMenu("šalį", async delegate (Dictionary<string, object> fields) {
					// add the newly created element to the db
					var elem = await MainManager.instance.client.CreateCountry(fields["Pavadinimas"] as string);

					updateData();
				})
				.AddStringField("Pavadinimas");
				break;

			case options.Cities:
				var countryNames = new List<string>();

				var countries = await MainManager.instance.client.GetCountries();

				foreach (var country in countries) {
					countryNames.Add(country.Name);
				}

				MainManager.instance.CreateInputMenu("meistą", async delegate (Dictionary<string, object> fields) {
					// add the newly created element to the db
					var elem = await MainManager.instance.client.CreateCity(fields["Pavadinimas"] as string, (int)fields["Šalis"]);

					updateData();
				})
				.AddStringField("Pavadinimas")
				.AddDropdownField("Šalis", countryNames);
				break;

			case options.Points:
				var pointTypes = await MainManager.instance.client.GetPointTypes();
				var cities = await MainManager.instance.client.GetCities();

				var list1 = new Dictionary<int, string>();
				var list2 = new Dictionary<int, string>();

				foreach (var type in pointTypes)
					list1[type.PointTypeID] = type.PointTypeName;

				foreach (var city in cities)
					list2[city.CityID] = city.Name;

				MainManager.instance.CreateInputMenu("tašką", async delegate (Dictionary<string, object> fields) {
					// add the newly created element to the db
					var elem = await MainManager.instance.client.CreatePoint(
						list1.ElementAt((int)fields["Taško tipas"]).Key,
						list2.ElementAt((int)fields["Miestas"]).Key,
						fields["Pavadinimas"] as string,
						fields["Adresas"] as string,
						(float)fields["Kaina"]
					);

					foreach (var img in (List<Dictionary<string, object>>)fields["Nuotraukos"])
						await MainManager.instance.client.AddImage(elem.PointID, img["Nuoroda"] as string);

					updateData();
				})
				.AddDropdownField("Taško tipas", list1.Values.ToList())
				.AddDropdownField("Miestas", list2.Values.ToList())
				.AddStringField("Pavadinimas")
				.AddStringField("Adresas")
				.AddFloatField("Kaina")
				.AddArrayField("Nuotraukos", delegate (ArrayField field) {
					field.AddStringField("Nuoroda");
				});
				break;

			case options.TransportRent:
				list1 = new Dictionary<int, string>();
				list2 = new Dictionary<int, string>();

				countries = await MainManager.instance.client.GetCountries();
				var trTypes = await MainManager.instance.client.GetTransportationTypes();

				foreach (var country in countries) {
					list1.Add(country.CountryID, country.Name);
				}

				foreach (var trType in trTypes) {
					list2.Add(trType.TransportationTypeID, trType.Name);
				}

				MainManager.instance.CreateInputMenu("transporto nuomos įmonę", async delegate (Dictionary<string, object> fields) {
					// add the newly created element to the db
					var elem = await MainManager.instance.client.AddTransportationRent(
						list1.ElementAt((int)fields["Transporto tipas"]).Key,
						list2.ElementAt((int)fields["Šalis"]).Key,
						fields["Įmonės pavadinimas"] as string,
						(float)fields["Kaina"]
					);

					updateData();
				})
				.AddStringField("Įmonės pavadinimas")
				.AddDropdownField("Šalis", list1.Values.ToList())
				.AddDropdownField("Transporto tipas", list2.Values.ToList())
				.AddFloatField("Kaina");
				break;
		}
	}
}
