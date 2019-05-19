using RunnerBLL.Abstract;
using System.IO;
using System.Xml.Serialization;

namespace RunnerBLL.Concrete
{
	public class XmlDeserializer<T> : Deserializer<T>
	{
		public XmlDeserializer(string filePath) : base(filePath)
		{

		}

		public override T GetEntity()
		{
			var serializer = new XmlSerializer(typeof(T));
			using (var reader = new StringReader(FileEntity.FileData))
			{
				return (T)serializer.Deserialize(reader);
			}
		}
	}
}
