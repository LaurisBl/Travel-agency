using GraphQL.Helpers;

namespace GraphQL.Model {
	public class Review {
		[GqlSelection("reviewID")] public int ReviewID { get; private set; }
		[GqlSelection("tripID")] public int TripID { get; private set; }
		[GqlSelection("rating")] public int Rating { get; private set; }
		[GqlSelection("description")] public string Description { get; private set; }
	}
}