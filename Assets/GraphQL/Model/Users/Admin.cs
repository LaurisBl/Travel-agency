using GraphQL.Helpers;

namespace GraphQL.Model {public class Admin {
        [GqlSelection("adminID")] public int AdminID { get; private set; }
        [GqlSelection("fName")] public string FName { get; private set; }
        [GqlSelection("eMail")] public string EMail { get; private set; }
        [GqlSelection("passwordKey")] public string PasswordKey { get; private set; }
    }
}