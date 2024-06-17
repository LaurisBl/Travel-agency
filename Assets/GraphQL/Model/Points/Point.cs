using GraphQL.Helpers;

namespace GraphQL.Model {
	public class Point {
		[GqlSelection("pointID")] public int PointID { get; private set; }
		[GqlSelection("pointTypeID")] public int PointTypeID { get; private set; }
		[GqlSelection("cityID")] public int CityID { get; private set; }
		[GqlSelection("name")] public string Name { get; private set; }
		[GqlSelection("address")] public string Address { get; private set; }
		[GqlSelection("price")] public float Price { get; private set; }
	}
}