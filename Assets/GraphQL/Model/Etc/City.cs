using GraphQL.Helpers;

namespace GraphQL.Model {
	public class City {
		[GqlSelection("cityID")] public int CityID { get; private set; }
		[GqlSelection("countryID")] public int CountryID { get; private set; }
		[GqlSelection("name")] public string Name { get; private set; }
	}
}