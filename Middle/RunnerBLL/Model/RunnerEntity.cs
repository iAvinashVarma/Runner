using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace RunnerBLL.Model
{
	public class RunnerEntity
	{
		[JsonIgnore]
		[JsonProperty(PropertyName = "postRunner")]
		public PostRunner PostRunner { get; set; }

		[JsonRequired]
		[JsonProperty(PropertyName = "runner")]
		public MainRunner MainRunner { get; set; }

		[JsonIgnore]
		[JsonProperty(PropertyName = "preRunner")]
		public PreRunner PreRunner { get; set; }
	}

	public class PostRunner
	{
		[JsonProperty(PropertyName = "add")]
		public List<RunnerData> PostRunnerDataList { get; set; }
	}

	public class PreRunner
	{
		[JsonProperty(PropertyName = "add")]
		public List<RunnerData> PreRunnerDataList { get; set; }
	}

	public class MainRunner
	{
		[JsonProperty(PropertyName = "add")]
		public List<RunnerData> MainRunnerDataList { get; set; }
	}

	public class RunnerData
	{
		[JsonRequired]
		[JsonProperty(PropertyName = "process")]
		public string Process { get; set; }

		[JsonRequired]
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[DefaultValue(false)]
		[JsonProperty(PropertyName = "enable")]
		public bool Enable { get; set; }

		[DefaultValue(0)]
		[JsonProperty(PropertyName = "sequence")]
		public int Sequence { get; set; }

		[JsonIgnore]
		public string TypeName => !string.IsNullOrEmpty(Type) ? Type.IndexOf(',') != -1 ? Type.Split(',')[0] : Type : string.Empty;

		[JsonIgnore]
		public string AssemblyName => !string.IsNullOrEmpty(Type) ? Type.IndexOf(',') != -1 ? Type.Split(',')[1] : Type.Substring(0, Type.LastIndexOf('.')) : string.Empty;

		public override string ToString()
		{
			return Type;
		}
	}
}