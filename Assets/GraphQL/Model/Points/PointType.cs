using GraphQL.Helpers;

namespace GraphQL.Model {
	public class PointType {
		[GqlSelection("pointTypeID")] public int PointTypeID { get; private set; }
		[GqlSelection("pointTypeName")] public string PointTypeName { get; private set; }
	}
}