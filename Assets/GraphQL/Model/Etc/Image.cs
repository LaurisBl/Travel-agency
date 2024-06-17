using GraphQL.Helpers;

namespace GraphQL.Model {
	public class Image {
		[GqlSelection("imageID")] public int ImageID { get; private set; }
		[GqlSelection("pointID")] public int PointID { get; private set; }
		[GqlSelection("imageLink")] public string ImageLink { get; private set; }
	}
}