using GraphQL.Helpers;

namespace GraphQL.Model {
	public class Trip {
		[GqlSelection("tripID")] public int TripID { get; private set; }
		[GqlSelection("userID")] public int UserID { get; private set; }
		[GqlSelection("name")] public string Name { get; private set; }
		[GqlSelection("guideID")] public int GuideID { get; private set; }
		[GqlSelection("consultantID")] public int ConsultantID { get; private set; }
		[GqlSelection("dateTimeID")] public int DateTimeID { get; private set; }
		[GqlSelection("transportRentID")] public int TransportRentID { get; private set; }
	}
}