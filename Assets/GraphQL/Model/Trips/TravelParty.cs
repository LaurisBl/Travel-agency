using GraphQL.Helpers;

namespace GraphQL.Model {
	public class TravelParty {
		[GqlSelection("travelPartyID")] public int TravelPartyID { get; private set; }
		[GqlSelection("tripID")] public int TripID { get; private set; }
		[GqlSelection("amount")] public int Amount { get; private set; }
	}
}