using System;

namespace GraphQL.Helpers {
	public class GraphQLException : Exception {
		public string ActualRequestBody { get; }
		public string ActualResponseBody { get; }

		internal GraphQLException (string message, string actualRequestBody, string actualResponseBody) : base(message) {
			ActualRequestBody = actualRequestBody;
			ActualResponseBody = actualResponseBody;
		}
	}
}

