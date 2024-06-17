using GraphQL.Helpers;

namespace GraphQL.Model {
	public class Price {
		[GqlSelection("priceID")] public int PriceID { get; private set; }
		[GqlSelection("tripID")] public int TripID { get; private set; }
		[GqlSelection("price")] public float _Price { get; private set; }
	}
}