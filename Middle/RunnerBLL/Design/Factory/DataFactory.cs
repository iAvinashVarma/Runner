using RunnerBLL.Concrete;
using RunnerBLL.Interface;
using RunnerBLL.Model;

namespace RunnerBLL.Design.Factory
{
	public class DataFactory : SingletonBase<DataFactory>
	{
		public IDeserializer<T> GetDeserializer<T>(string filePath)
		{
			IDeserializer<T> deserializer = null;
			FileEntity fileEntity = new FileEntity(filePath);
			switch (fileEntity.FileType)
			{
				case FileType.XML:
					break;

				case FileType.JSON:
					deserializer = new JsonDeserializer<T>(filePath);
					break;

				case FileType.CSV:
					break;

				default:
					break;
			}
			return deserializer;
		}
	}
}