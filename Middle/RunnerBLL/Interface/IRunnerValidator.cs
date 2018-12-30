﻿using System.Collections;

namespace RunnerBLL.Interface
{
	public interface IRunnerValidator
	{
		int ValidateSequence { get; }

		bool IsValid(Hashtable hashtable);
	}
}