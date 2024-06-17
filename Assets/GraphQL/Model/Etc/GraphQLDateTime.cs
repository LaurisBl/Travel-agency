using GraphQL.Helpers;

namespace GraphQL.Model {
	public class GraphQLDateTime {
		[GqlSelection("dateTimeID")] public int DateTimeID { get; private set; }
		[GqlSelection("timeStamp")] public string Timestamp { get; private set; }
	}
}