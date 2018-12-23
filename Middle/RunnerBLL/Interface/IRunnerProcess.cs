using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnerBLL.Interface
{
	public interface IRunnerProcess
	{
		Hashtable Hashtable
		{
			get; set;
		}

		bool Validate();

		void Start();
	}
}
