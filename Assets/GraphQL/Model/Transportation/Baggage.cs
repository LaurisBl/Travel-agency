using GraphQL.Helpers;

namespace GraphQL.Model {
	public class Baggage {
		[GqlSelection("baggageID")] public int BaggageID { get; private set; }
		[GqlSelection("tripID")] public int TripID { get; private set; }
		[GqlSelection("transportTypeID")] public int TransportTypeID { get; private set; }
		[GqlSelection("amount")] public int Amount { get; private set; }
		[GqlSelection("weight")] public int Weight { get; private set; }
	}
}