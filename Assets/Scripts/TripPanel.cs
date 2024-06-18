using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TripPanel : MonoBehaviour {
    private int _tripID;

    public TMP_Text Title;
    public TMP_Text Info;
    public TMP_Text StartingPoint;

    public RectTransform PointArray;

    public Transform pointPrefab;

    public GameObject BackButton;

    public async void Open(int tripID, bool user = true) {
        // Set trip ID
        _tripID = tripID;

        // Get trip info
        var trip = await MainManager.instance.client.GetTrip(tripID);

		// Get trip points
		var points = await MainManager.instance.client.FindPointsForTrip(tripID);

		// Clear point array
        foreach (Transform child in PointArray)
            if(!child.CompareTag("Don\\t remove"))
                Destroy(child.gameObject);

        // Assign values
        Title.text = trip.Name;

        var guide = await MainManager.instance.client.GetGuide(trip.GuideID);
        var consultant = await MainManager.instance.client.GetConsultant(trip.ConsultantID);
        var date = new System.DateTime(long.Parse((await MainManager.instance.client.GetDateTime(trip.DateTimeID)).Timestamp));
        var transportation = await MainManager.instance.client.GetTransportationRent(trip.TransportRentID);

        Info.text = string.Format("Gidas: {0}\nKonsultantas: {1}\nData: {2}\nTransportacija: {3}", new object[4] { guide.FName, consultant.FName, date.ToString("yyyy/MM/dd"), transportation.CompanyName });
        if(!user) {
            var use = await MainManager.instance.client.GetUser(trip.UserID);

            Info.text = string.Format("Vartotojas: {0}\nEl paštas: {1}\n\n{2}", use.FName, use.EMail, Info.text);
        }


        // Populate point array
        StartingPoint.text = (await MainManager.instance.client.GetPoint(points[0].PointID)).Name;

        points.RemoveAt(0);
        foreach(var point in points) {
            var pointObj = Instantiate(pointPrefab, PointArray);

            var label = (await MainManager.instance.client.GetPoint(points[0].PointID)).Name;

            pointObj.name = label;

            pointObj.GetChild(3).GetComponent<TMP_Text>().text = label;
		}
	}

	public void OnEnable () {
        BackButton.SetActive(true);
	}

	public void OnDisable () {
		BackButton.SetActive(false);
	}
}
