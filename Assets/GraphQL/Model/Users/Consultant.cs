using GraphQL.Helpers;

namespace GraphQL.Model {
	public class Consultant {
		[GqlSelection("consultantID")] public int ConsultantID { get; private set; }
		[GqlSelection("fName")] public string FName { get; private set; }
		[GqlSelection("eMail")] public string EMail { get; private set; }
		[GqlSelection("passwordKey")] public string PasswordKey { get; private set; }
	}
}