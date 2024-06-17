using GraphQL.Helpers;
using GraphQL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQL {
	public partial class Client {

		#region Authentication

		public async Task<bool> TryLogin (string email, string password) {
			var selections = new GqlSelection("login") {
				Parameters = new List<GqlParameter>{
					new GqlParameter("eMail", email),
					new GqlParameter("passwordKey", password)
				}
			};
			try {
				var data = await PostRequestAsync(selections, true);

				token = (string)data["login"];

				return true;
			} catch {
				return false;
			}
		}

		public async Task<bool> TryRegister (string fname, string email, string password, int userType) {
			if (await TryLogin(email, password)) { token = null; return false; }

			string mutation = userType switch {
				0 => "createUser",
				1 => "createGuide",
				2 => "createConsultant",
				_ => string.Empty
			};

			var selections = new GqlSelection(mutation) {
				Parameters = new List<GqlParameter>{
					new GqlParameter("fName", fname),
					new GqlParameter("eMail", email),
					new GqlParameter("passwordKey", password)
				},
				Selections = new List<GqlSelection> {
					new GqlSelection("fName")
				}
			};

			var data = await PostRequestAsync(selections, true);

			return await TryLogin(email, password);
		}

		#endregion

		#region Trips

		public async Task<Trip> CreateTrip (int userID, string name, int guideID, int consultantID, int dateTimeID, int transportRentID) {
			var selections = new GqlSelection("createTrip") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("userID", userID),
					new GqlParameter("name", name),
					new GqlParameter("guideID", guideID),
					new GqlParameter("consultantID", consultantID),
					new GqlParameter("dateTimeID", dateTimeID),
					new GqlParameter("transportRentID", transportRentID)
				},
				Selections = GqlParser.ParseToSelections<Trip>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<Trip>(data["createTrip"]);
		}

		public async Task<Review> CreateReview (int tripID, int rating, string description) {
			var selections = new GqlSelection("createReview") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("tripID", tripID),
					new GqlParameter("rating", rating),
					new GqlParameter("description", description)
				},
				Selections = GqlParser.ParseToSelections<Review>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<Review>(data["createReview"]);
		}

		public async Task<TravelParty> AddTravelParty (int tripID, int amount) {
			var selections = new GqlSelection("addTravelParty") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("tripID", tripID),
					new GqlParameter("amount", amount)
				},
				Selections = GqlParser.ParseToSelections<TravelParty>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<TravelParty>(data["addTravelParty"]);
		}

		public async Task<Price> SetPrice (int tripID, int price) {
			var selections = new GqlSelection("setPrice") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("tripID", tripID),
					new GqlParameter("price", price)
				},
				Selections = GqlParser.ParseToSelections<Price>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<Price>(data["setPrice"]);
		}

		#endregion

		#region Transportation

		public async Task<Baggage> AddBaggage (int tripID, int transportTypeID, int amount, int weight) {
			var selections = new GqlSelection("addBaggage") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("tripID", tripID),
					new GqlParameter("transportTypeID", transportTypeID),
					new GqlParameter("amount", amount),
					new GqlParameter("weight", weight)
				},
				Selections = GqlParser.ParseToSelections<Baggage>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<Baggage>(data["addBaggage"]);
		}

		public async Task<TransportationRent> AddTransportationRent (int countryID, int transportTypeID, string companyName, double price) {
			var selections = new GqlSelection("addTransportationRent") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("countryID", countryID),
					new GqlParameter("transportationTypeID", transportTypeID),
					new GqlParameter("companyName", companyName),
					new GqlParameter("price", price)
				},
				Selections = GqlParser.ParseToSelections<TransportationRent>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<TransportationRent>(data["addTransportationRent"]);
		}

		public async Task<bool> RemoveTransportationRent (int transportationRentID) {
			var selections = new GqlSelection("removeTransportationRent") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("transportationRentID", transportationRentID)
				}
			};

			var data = await PostRequestAsync(selections, true);

			return (string)data["removeTransportationRent"] == "OK";
		}

		#endregion

		#region Points

		public async Task<TripPoint> AddPointToTrip (int pointID, int tripID) {
			var selections = new GqlSelection("addPointToTrip") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("pointID", pointID),
					new GqlParameter("tripID", tripID)
				},
				Selections = GqlParser.ParseToSelections<TripPoint>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<TripPoint>(data["addPointToTrip"]);
		}

		public async Task<Point> CreatePoint (int pointTypeID, int cityID, string name, string address, float price) {
			var selections = new GqlSelection("createPoint") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("pointTypeID", pointTypeID),
					new GqlParameter("cityID", cityID),
					new GqlParameter("name", name),
					new GqlParameter("address", address),
					new GqlParameter("price", price)
				},
				Selections = GqlParser.ParseToSelections<Point>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<Point>(data["createPoint"]);
		}

		public async Task<bool> DeletePoint (int pointID) {
			var selections = new GqlSelection("deletePoint") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("pointID", pointID)
				}
			};

			var data = await PostRequestAsync(selections, true);

			return (string)data["deletePoint"] == "OK";
		}

		#endregion

		#region Etc

		public async Task<Country> CreateCountry (string name) {
			var selections = new GqlSelection("createCountry") {
				Parameters = new List<GqlParameter> { new GqlParameter("name", name) },
				Selections = GqlParser.ParseToSelections<Country>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<Country>(data["createCountry"]);
		}

		public async Task<bool> DeleteCountry (int countryID) {
			var selections = new GqlSelection("deleteCountry") {
				Parameters = new List<GqlParameter> { new GqlParameter("countryID", countryID) }
			};

			var data = await PostRequestAsync(selections, true);

			return (string)data["deleteCountry"] == "OK";
		}

		public async Task<City> CreateCity (string name, int countryID) {
			var selections = new GqlSelection("createCity") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("name", name),
					new GqlParameter("countryID", countryID)
				},
				Selections = GqlParser.ParseToSelections<City>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<City>(data["createCity"]);
		}

		public async Task<bool> DeleteCity (int cityID) {
			var selections = new GqlSelection("deleteCity") {
				Parameters = new List<GqlParameter> { new GqlParameter("cityID", cityID) }
			};

			var data = await PostRequestAsync(selections, true);

			return (string)data["deleteCity"] == "OK";
		}

		public async Task<Image> AddImage (int pointID, string link) {
			var selections = new GqlSelection("addImage") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("pointID", pointID),
					new GqlParameter ("imageLink", link)
				},
				Selections = GqlParser.ParseToSelections<Image>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<Image>(data["addImage"]);
		}

		public async Task<bool> RemoveImagesForPoint (int pointID) {
			var selections = new GqlSelection("removeImagesForPoint") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("pointID", pointID)
				}
			};

			var data = await PostRequestAsync(selections, true);

			return (string)data["removeImagesForPoint"] == "OK";
		}

		public async Task<GraphQLDateTime> AddDateTime (long timeStamp) {
			var selections = new GqlSelection("addDateTime") {
				Parameters = new List<GqlParameter> {
					new GqlParameter("timeStamp", timeStamp.ToString()),
				},
				Selections = GqlParser.ParseToSelections<GraphQLDateTime>()
			};

			var data = await PostRequestAsync(selections, true);

			return GqlParser.ParseFromJson<GraphQLDateTime>(data["addDateTime"]);
		}

		#endregion
	}
}
