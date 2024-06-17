using GraphQL.Helpers;

namespace GraphQL.Model {
	public class TransportationType {
		[GqlSelection("transportationTypeID")] public int TransportationTypeID { get; private set; }
		[GqlSelection("name")] public string Name { get; private set; }
	}
}