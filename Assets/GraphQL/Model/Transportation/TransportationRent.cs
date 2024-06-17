using GraphQL.Helpers;

namespace GraphQL.Model {
	public class TransportationRent {
		[GqlSelection("transportationRentID")] public int TransportationRentID { get; private set; }
		[GqlSelection("countryID")] public int CountryID { get; private set; }
		[GqlSelection("transportationTypeID")] public int TransportationTypeID { get; private set; }
		[GqlSelection("companyName")] public string CompanyName { get; private set; }
		[GqlSelection("price")] public float Price { get; private set; }
	}
}