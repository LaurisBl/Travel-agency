using GraphQL.Helpers;
using GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQL {
	public partial class Client {
		#region Authentication

		public string getKey () {
			return token;
		}

		public async Task<bool> setKey (string _token) {
			token = _token;

			try {
				await me();

				return true;
			} catch {
				token = null;

				return false;
			}
		}

		public async Task<int> me () {
			if (token == null) { throw new Exception("User is not logged in"); }

			var selections = new GqlSelection("me")
			{
				Selections = new List<GqlSelection> { new GqlSelection("type") }
			};

			return (int)(await PostRequestAsync(selections))["me"]["type"];
		}

		public async Task<int> getID () {
			if (token == null) { throw new Exception("User is not logged in"); }

			var selections = new GqlSelection("me")
			{
				Selections = new List<GqlSelection> { new GqlSelection("ID") }
			};

			return (int)(await PostRequestAsync(selections))["me"]["ID"];
		}

		#endregion

		#region Users

		public async Task<List<User>> GetUsers () {
			var selections = new GqlSelection("getUsers")
	{
				Selections = GqlParser.ParseToSelections<User>()
			};
			var data = await PostRequestAsync(selections);

			return new List<User>(GqlParser.ParseFromJson<User[]>(data["getUsers"]));
		}

		public async Task<User> GetUser (int userID) {
			var selections = new GqlSelection("getUser")
	{
				Selections = GqlParser.ParseToSelections<User>(),
				Parameters = new List<GqlParameter> { new GqlParameter("userID", userID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<User>(data["getUser"]);
		}

		public async Task<List<Admin>> GetAdmins () {
			var selections = new GqlSelection("getAdmins")
	{
				Selections = GqlParser.ParseToSelections<Admin>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Admin>(GqlParser.ParseFromJson<Admin[]>(data["getAdmins"]));
		}

		public async Task<Admin> GetAdmin (int adminID) {
			var selections = new GqlSelection("getAdmin")
	{
				Selections = GqlParser.ParseToSelections<Admin>(),
				Parameters = new List<GqlParameter> { new GqlParameter("adminID", adminID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<Admin>(data["getAdmin"]);
		}

		public async Task<List<Guide>> GetGuides () {
			var selections = new GqlSelection("getGuides")
	{
				Selections = GqlParser.ParseToSelections<Guide>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Guide>(GqlParser.ParseFromJson<Guide[]>(data["getGuides"]));
		}

		public async Task<Guide> GetGuide (int guideID) {
			var selections = new GqlSelection("getGuide")
	{
				Selections = GqlParser.ParseToSelections<Guide>(),
				Parameters = new List<GqlParameter> { new GqlParameter("guideID", guideID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<Guide>(data["getGuide"]);
		}

		public async Task<List<Consultant>> GetConsultants () {
			var selections = new GqlSelection("getConsultants")
	{
				Selections = GqlParser.ParseToSelections<Consultant>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Consultant>(GqlParser.ParseFromJson<Consultant[]>(data["getConsultants"]));
		}

		public async Task<Consultant> GetConsultant (int consultantID) {
			var selections = new GqlSelection("getConsultant")
	{
				Selections = GqlParser.ParseToSelections<Consultant>(),
				Parameters = new List<GqlParameter> { new GqlParameter("consultantID", consultantID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<Consultant>(data["getConsultant"]);
		}

		#endregion

		#region Trips

		public async Task<List<Review>> GetReviews () {
			var selections = new GqlSelection("getReviews")
	{
				Selections = GqlParser.ParseToSelections<Review>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Review>(GqlParser.ParseFromJson<Review[]>(data["getReviews"]));
		}

		public async Task<Review> GetReview (int reviewID) {
			var selections = new GqlSelection("getReview")
	{
				Selections = GqlParser.ParseToSelections<Review>(),
				Parameters = new List<GqlParameter> { new GqlParameter("reviewID", reviewID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<Review>(data["getReview"]);
		}

		public async Task<List<TravelParty>> GetTravelParties () {
			var selections = new GqlSelection("getTravelParties")
	{
				Selections = GqlParser.ParseToSelections<TravelParty>()
			};
			var data = await PostRequestAsync(selections);

			return new List<TravelParty>(GqlParser.ParseFromJson<TravelParty[]>(data["getTravelParties"]));
		}

		public async Task<TravelParty> GetTravelParty (int travelPartyID) {
			var selections = new GqlSelection("getTravelParty")
	{
				Selections = GqlParser.ParseToSelections<TravelParty>(),
				Parameters = new List<GqlParameter> { new GqlParameter("travelPartyID", travelPartyID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<TravelParty>(data["getTravelParty"]);
		}

		public async Task<List<Trip>> GetTrips () {
			var selections = new GqlSelection("getTrips")
	{
				Selections = GqlParser.ParseToSelections<Trip>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Trip>(GqlParser.ParseFromJson<Trip[]>(data["getTrips"]));
		}

		public async Task<Trip> GetTrip (int tripID) {
			var selections = new GqlSelection("getTrip")
	{
				Selections = GqlParser.ParseToSelections<Trip>(),
				Parameters = new List<GqlParameter> { new GqlParameter("tripID", tripID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<Trip>(data["getTrip"]);
		}

		public async Task<List<Trip>> FindTrips (int userID) {
			var selections = new GqlSelection("findTrips")
	{
				Parameters = new List<GqlParameter> { new GqlParameter("userID", userID) },
				Selections = GqlParser.ParseToSelections<Trip>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Trip>(GqlParser.ParseFromJson<Trip[]>(data["findTrips"]));
		}

		public async Task<List<Trip>> FindTripsForGuide (int guideID) {
			var selections = new GqlSelection("findTripsForGuide")
	{
				Parameters = new List<GqlParameter> { new GqlParameter("guideID", guideID) },
				Selections = GqlParser.ParseToSelections<Trip>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Trip>(GqlParser.ParseFromJson<Trip[]>(data["findTripsForGuide"]));
		}

		public async Task<List<Trip>> FindTripsForConsultant (int consultantID) {
			var selections = new GqlSelection("findTripsForConsultant")
	{
				Parameters = new List<GqlParameter> { new GqlParameter("consultantID", consultantID) },
				Selections = GqlParser.ParseToSelections<Trip>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Trip>(GqlParser.ParseFromJson<Trip[]>(data["findTripsForConsultant"]));
		}

		public async Task<Price> GetPrice (int priceID) {
			var selections = new GqlSelection("getPrice")
	{
				Selections = GqlParser.ParseToSelections<Price>(),
				Parameters = new List<GqlParameter> { new GqlParameter("priceID", priceID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<Price>(data["getPrice"]);
		}

		public async Task<Price> FindPrice (int tripID) {
			var selections = new GqlSelection("findPrice")
	{
				Selections = GqlParser.ParseToSelections<Price>(),
				Parameters = new List<GqlParameter> { new GqlParameter("tripID", tripID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<Price>(data["findPrice"]);
		}

		#endregion

		#region Transportation

		public async Task<List<Baggage>> GetBaggages () {
			var selections = new GqlSelection("getBaggages")
	{
				Selections = GqlParser.ParseToSelections<Baggage>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Baggage>(GqlParser.ParseFromJson<Baggage[]>(data["getBaggages"]));
		}

		public async Task<Baggage> GetBaggage (int baggageID) {
			var selections = new GqlSelection("getBaggage")
	{
				Selections = GqlParser.ParseToSelections<Baggage>(),
				Parameters = new List<GqlParameter> { new GqlParameter("baggageID", baggageID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<Baggage>(data["getBaggage"]);
		}

		public async Task<List<Baggage>> FindBaggage (int tripID) {
			var selections = new GqlSelection("findBaggage")
	{
				Selections = GqlParser.ParseToSelections<Baggage>(),
				Parameters = new List<GqlParameter> { new GqlParameter("tripID", tripID) }
			};
			var data = await PostRequestAsync(selections);

			return new (GqlParser.ParseFromJson<Baggage[]>(data["findBaggage"]));
		}

		public async Task<List<TransportationType>> GetTransportationTypes () {
			var selections = new GqlSelection("getTransportationTypes")
	{
				Selections = GqlParser.ParseToSelections<TransportationType>()
			};
			var data = await PostRequestAsync(selections);

			return new List<TransportationType>(GqlParser.ParseFromJson<TransportationType[]>(data["getTransportationTypes"]));
		}

		public async Task<TransportationType> GetTransportationType (int transportationTypeID) {
			var selections = new GqlSelection("getTransportationType")
{
				Selections = GqlParser.ParseToSelections<TransportationType>(),
				Parameters = new List<GqlParameter> { new GqlParameter("transportationTypeID", transportationTypeID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<TransportationType>(data["getTransportationType"]);
		}

		public async Task<List<TransportationRent>> GetTransportationRentList () {
			var selections = new GqlSelection("getTransportationRentList")
	{
				Selections = GqlParser.ParseToSelections<TransportationRent>()
			};
			var data = await PostRequestAsync(selections);

			return new List<TransportationRent>(GqlParser.ParseFromJson<TransportationRent[]>(data["getTransportationRentList"]));
		}

		public async Task<TransportationRent> GetTransportationRent (int transportationRentID) {
			var selections = new GqlSelection("getTransportationRent")
{
				Selections = GqlParser.ParseToSelections<TransportationRent>(),
				Parameters = new List<GqlParameter> { new GqlParameter("transportationRentID", transportationRentID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<TransportationRent>(data["getTransportationRent"]);
		}

		#endregion

		#region Points

		public async Task<List<PointType>> GetPointTypes () {
			var selections = new GqlSelection("getPointTypes")
	{
				Selections = GqlParser.ParseToSelections<PointType>()
			};
			var data = await PostRequestAsync(selections);

			return new List<PointType>(GqlParser.ParseFromJson<PointType[]>(data["getPointTypes"]));
		}

		public async Task<List<Point>> GetPoints () {
			var selections = new GqlSelection("getPoints")
	{
				Selections = GqlParser.ParseToSelections<Point>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Point>(GqlParser.ParseFromJson<Point[]>(data["getPoints"]));
		}

		public async Task<Point> GetPoint (int pointID) {
			var selections = new GqlSelection("getPoint")
	{
				Selections = GqlParser.ParseToSelections<Point>(),
				Parameters = new List<GqlParameter> { new GqlParameter("pointID", pointID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<Point>(data["getPoint"]);
		}

		public async Task<List<TripPoint>> FindPointsForTrip (int tripID) {
			var selections = new GqlSelection("findPointsForTrip")
	{
				Parameters = new List<GqlParameter> { new GqlParameter("tripID", tripID) },
				Selections = GqlParser.ParseToSelections<TripPoint>()
			};
			var data = await PostRequestAsync(selections);

			return new (GqlParser.ParseFromJson<TripPoint[]>(data["findPointsForTrip"]));
		}

		#endregion

		#region Etc

		public async Task<List<City>> GetCities () {
			var selections = new GqlSelection("getCities")
	{
				Selections = GqlParser.ParseToSelections<City>()
			};
			var data = await PostRequestAsync(selections);

			return new List<City>(GqlParser.ParseFromJson<City[]>(data["getCities"]));
		}

		public async Task<City> GetCity (int cityID) {
			var selections = new GqlSelection("getCity")
	{
				Selections = GqlParser.ParseToSelections<City>(),
				Parameters = new List<GqlParameter> { new GqlParameter("cityID", cityID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<City>(data["getCity"]);
		}

		public async Task<List<Country>> GetCountries () {
			var selections = new GqlSelection("getCountries")
	{
				Selections = GqlParser.ParseToSelections<Country>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Country>(GqlParser.ParseFromJson<Country[]>(data["getCountries"]));
		}

		public async Task<Country> GetCountry (int countryID) {
			var selections = new GqlSelection("getCountry")
	{
				Selections = GqlParser.ParseToSelections<Country>(),
				Parameters = new List<GqlParameter> { new GqlParameter("countryID", countryID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<Country>(data["getCountry"]);
		}

		public async Task<List<Image>> GetImages () {
			var selections = new GqlSelection("getImages")
	{
				Selections = GqlParser.ParseToSelections<Image>()
			};
			var data = await PostRequestAsync(selections);

			return new List<Image>(GqlParser.ParseFromJson<Image[]>(data["getImages"]));
		}

		public async Task<Image> GetImage (int imageID) {
			var selections = new GqlSelection("getImage")
	{
				Selections = GqlParser.ParseToSelections<Image>(),
				Parameters = new List<GqlParameter> { new GqlParameter("imageID", imageID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<Image>(data["getImage"]);
		}

		public async Task<GraphQLDateTime> GetDateTime (int dateTimeID) {
			var selections = new GqlSelection("getDateTime")
	{
				Selections = GqlParser.ParseToSelections<GraphQLDateTime>(),
				Parameters = new List<GqlParameter> { new GqlParameter("dateTimeID", dateTimeID) }
			};
			var data = await PostRequestAsync(selections);

			return GqlParser.ParseFromJson<GraphQLDateTime>(data["getDateTime"]);
		}

		#endregion
	}
}

