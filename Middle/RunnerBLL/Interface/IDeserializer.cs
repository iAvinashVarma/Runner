using RunnerBLL.Abstract;
using RunnerBLL.Model;

namespace RunnerBLL.Interface
{
	public interface IDeserializer<T>
	{
		FileEntity FileEntity { get; set; }

		T GetEntity();
	}
}
