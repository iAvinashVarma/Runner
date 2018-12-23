using Newtonsoft.Json;
using RunnerBLL.Abstract;
using RunnerBLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnerBLL.Concrete
{
	public class JsonDeserializer<T> : Deserializer<T>
	{
		public JsonDeserializer(string filePath) : base(filePath)
		{
		}

		public override T GetEntity()
		{
			return JsonConvert.DeserializeObject<T>(FileEntity.FileData);
		}
	}
}
