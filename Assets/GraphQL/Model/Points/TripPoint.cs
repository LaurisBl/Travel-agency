using GraphQL.Helpers;

namespace GraphQL.Model {
	public class TripPoint {
		[GqlSelection("tripPointID")] public int TripPointID { get; private set; }
		[GqlSelection("pointID")] public int PointID { get; private set; }
		[GqlSelection("tripID")] public string TripID { get; private set; }
	}
}