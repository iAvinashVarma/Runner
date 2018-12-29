using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnerBLL.Interface
{
	public interface IRunnerValidator
	{
		bool IsValid(Hashtable hashtable);
	}
}
