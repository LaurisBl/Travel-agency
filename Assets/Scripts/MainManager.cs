using GraphQL;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

	[Header("Prefabs")]
	public Transform modulePrefab;
	public ArrayField arrayFieldPrefab;
	public Transform itemPrototypePrefab;
	public StringField stringFieldPrefab;
	public StringField emailFieldPrefab;
	public IntField intFieldPrefab;
	public FloatField floatFieldPrefab;
	public DropdownField dropdownFieldPrefab;
	public DateField dateFieldPrefab;

	[Space(15)]

	[HideInInspector] public Client client;

    [SerializeField] private InputMenu inputMenuPrefab;

    [SerializeField] private RectTransform canvas;

	[Space(15)]

	public MenuSystem menuManager;

	[Header("Fields")]
	[SerializeField] private TMP_Dropdown regType;
	[SerializeField] private TMP_InputField regFName;
	[SerializeField] private TMP_InputField regEmail;
	[SerializeField] private TMP_InputField regPassword;
	[SerializeField] private GameObject regErrorMenu;

	[Space(15)]

	[SerializeField] private TMP_InputField loginEmail;
	[SerializeField] private TMP_InputField loginPassword;
	[SerializeField] private GameObject loginErrorMenu;

	[SerializeField] private MenuSystem HomeScreenNavigation;
	[SerializeField] private List<ListMenu> homeScreenMenuLists = new();

	private async void Awake () {
		instance = this;

		client = new Client();

		if (PlayerPrefs.HasKey("key")) {
			if (await client.setKey(PlayerPrefs.GetString("key", ""))) {
				menuManager.loadMenu(3);
				Home();
			}
		} else {
			menuManager.loadMenu(0);
		}

	}

	private async void LoadTrips() {
		var clientType = await client.me();
		var clientID = await client.getID();

		var list = clientType switch {
			3 => null,
			_ => homeScreenMenuLists[clientType]
		};


		if (!list)
			return;

		var trips = clientType switch {
			0 => await client.FindTrips(clientID),
			1 => await client.FindTripsForGuide(clientID),
			2 => await client.FindTripsForConsultant(clientID),
			_ => null
		};

		list.ClearList();

		foreach (var trip in trips) {
			var item = Instantiate(list.ItemPrefab, list.ItemList);

			var points = await client.FindPointsForTrip(trip.TripID);
			var date = await client.GetDateTime(trip.DateTimeID);

			var firstPoint = await client.GetPoint(points[0].PointID);
			var lastPoint = await client.GetPoint(points[points.Count - 1].PointID);

			item.transform.GetChild(0).GetComponent<TMP_Text>().text = trip.Name;
			item.transform.GetChild(1).GetComponent<TMP_Text>().text = new System.DateTime(long.Parse(date.Timestamp)).ToString("yyyy/MM/dd");
			item.transform.GetChild(2).GetComponent<TMP_Text>().text = string.Format("{0} <mspace=7><voffset=2>---</voffset><space=-8.4>></pos></mspace> {1}", firstPoint.Name, lastPoint.Name);

			item.GetComponent<Button>().onClick.AddListener(() => { 
				HomeScreenNavigation.loadMenu(4); 
				HomeScreenNavigation.menus[4].GetComponent<TripPanel>().Open(trip.TripID, clientType == 0); 
			});
		}
	}

	public async void Home() {
		HomeScreenNavigation.loadMenu(await client.me());

		LoadTrips();
	}

	public void Logout() {
		PlayerPrefs.DeleteKey("key");
		menuManager.loadMenu(0);
	}

	public async void Login() {
		if(await client.TryLogin(loginEmail.text, loginPassword.text)) {
			menuManager.loadMenu(3);

			PlayerPrefs.SetString("key", client.getKey());
			PlayerPrefs.Save();

			Home();
		} else
			loginErrorMenu.SetActive(true);
	}

	public async void Register() {
		if (await client.TryRegister(regFName.text, regEmail.text, regPassword.text, regType.value)) {
			menuManager.loadMenu(3);

			PlayerPrefs.SetString("key", client.getKey());
			PlayerPrefs.Save();

			Home();
		} else
			regErrorMenu.SetActive(true);
	}

	public async void CreateTrip() {
		var list1 = new Dictionary<int, string>();
		var list2 = new Dictionary<int, string>();
		var list3 = new Dictionary<int, string>();
		var list4 = new Dictionary<int, string>();
		var list5 = new Dictionary<int, string>();

		var guides = await client.GetGuides();
		var consultants = await client.GetConsultants();
		var rents = await client.GetTransportationRentList();
		var points = await client.GetPoints();
		var transportTypes = await client.GetTransportationTypes();

		foreach (var guide in guides)
			list1.Add(guide.GuideID, guide.FName);

		foreach (var consultant in consultants)
			list2.Add(consultant.ConsultantID, consultant.FName);

		foreach (var rent in rents)
			list3.Add(rent.TransportationRentID, rent.CompanyName);

		foreach (var point in points)
			list4.Add(point.PointID, point.Name);

		foreach (var transportationType in transportTypes)
			list5.Add(transportationType.TransportationTypeID, transportationType.Name);

		CreateInputMenu("kelionę", async delegate(Dictionary<string, object> fields) {
			// Get values
			var tripName = fields["Pavadinimas"] as string;
			var guideID = list1.ElementAt((int)fields["Gidas"]).Key;
			var consultantID = list2.ElementAt((int)fields["Konsultantas"]).Key;
			var date = fields["Data"] as System.DateTime? ?? System.DateTime.Now;
			var rentID = list3.ElementAt((int)fields["Transporto nuoma"]).Key;
			var peopleCount = fields["Žmonių kiekis"] as int? ?? 0;

			// Create supporting elements
			var dateTime = await client.AddDateTime(date.Ticks);

			// Create the trip
			var trip = await client.CreateTrip(await client.getID(), tripName, guideID, consultantID, dateTime.DateTimeID, rentID);

			// Create the leading elements
			var group = await client.AddTravelParty(trip.TripID, peopleCount);

			// Go through arrays
			foreach (var item in (List<Dictionary<string, object>>)fields["Bagažas"]) {
				// Get values
				var transportType = list5.ElementAt((int)item["Transporto tipas"]).Key;
				var count = (int)item["Kiekis"];
				var weight = (int)item["Svoris"];

				// Create the item
				var baggage = await client.AddBaggage(trip.TripID, transportType, count, weight);
			}

			foreach (var item in (List<Dictionary<string, object>>)fields["Taškai"]) {
				// Get values
				var pointID = list4.ElementAt((int)item["Taškas"]).Key;

				// Create the item
				var tripPoint = await client.AddPointToTrip(pointID, trip.TripID);
			}

			LoadTrips();
		})
		.AddStringField("Pavadinimas")
		.AddDropdownField("Gidas", new(list1.Values))
		.AddDropdownField("Konsultantas", new(list2.Values))
		.AddDateField("Data")
		.AddDropdownField("Transporto nuoma", new(list3.Values))
		.AddArrayField("Bagažas", (ArrayField field) => {
			field.AddDropdownField("Transporto tipas", new(list5.Values))
			.AddIntField("Kiekis")
			.AddIntField("Svoris");
		})
		.AddArrayField("Taškai", (ArrayField field) => {
			field.AddDropdownField("Taškas", new(list4.Values));
		})
		.AddIntField("Žmonių kiekis");
	}

	private void Start () {
		/*CreateInputMenu("Hello world", delegate (Dictionary<string, object> dict) {
			Debug.Log("Hello world submitted");

			Debug.Log("Values");
			foreach(var (key, value) in dict) {
				Debug.Log($"{key}: {value}");
			}
		})
		.AddEmailField("Email")
		.AddDropdownField("Type", new() { "A", "B", "C" })
		.AddArrayField("Test", delegate (ArrayField field) {
				field.AddIntField("Test array item");
			})
		.AddStringField("Name", true);
		*/
	}

	public InputMenu CreateInputMenu (string menuName, UnityAction<Dictionary<string, object>> _callback) {
		var menu = Instantiate(inputMenuPrefab, canvas);

		menu.Initialize(menuName, _callback);

		return menu;
	}
}
