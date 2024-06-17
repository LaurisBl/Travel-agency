using GraphQL.Helpers;

namespace GraphQL.Model {
	public class Country {
		[GqlSelection("countryID")] public int CountryID { get; private set; }
		[GqlSelection("name")] public string Name { get; private set; }
	}
}