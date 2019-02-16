using Newtonsoft.Json;
using System.Collections.Generic;

namespace APIOperator.Model
{
	public class APIModel
	{
		[JsonProperty(PropertyName = "datum")]
		public List<APIDatum> APIData { get; set; }
	}

	public class APIDatum
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		[JsonProperty(PropertyName = "contentType")]
		public string ContentType { get; set; }
	}
}
