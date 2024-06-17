using System.Net.Http;
using System;
using GraphQL.Helpers;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GraphQL {
	public partial class Client {
		private string? token = null;
		private readonly HttpClient _client = new();
		private readonly Uri _url = new("http://localhost:9049/graphql");

		private async Task<JToken> PostRequestAsync (GqlSelection selection, bool isMutation = false) {
			var body = JObject.FromObject(new { query = (isMutation ? "mutation" : string.Empty) + selection });
			var bodyText = body["query"].ToObject<string>();
			var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
			if(token != null) content.Headers.Add("token", token);
			var response = await _client.PostAsync(_url, content);
			var responseText = await response.Content.ReadAsStringAsync();
			var json = JObject.Parse(responseText);
			if (!response.IsSuccessStatusCode)
				throw new GraphQLException(
					json["errors"].First["message"].ToString(),
					bodyText,
					responseText
				);

			return json["data"];
		}

		private async Task<JToken> GetSingleDataAsync (params GqlSelection[] path) {
			var selection = path[^1];
			for (var index = path.Length - 2; index >= 0; index--) {
				var newSelection = path[index];
				newSelection.Selections ??= new List<GqlSelection>();
				newSelection.Selections.Add(selection);
				selection = newSelection;
			}
			var token = await PostRequestAsync(selection);
			foreach (var item in path)
				token = token[item.Name];
			return token;
		}
	}

}


