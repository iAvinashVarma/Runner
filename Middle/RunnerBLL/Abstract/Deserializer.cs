﻿using RunnerBLL.Interface;
using RunnerBLL.Model;

namespace RunnerBLL.Abstract
{
	public abstract class Deserializer<T> : IDeserializer<T>
	{
		public FileEntity FileEntity { get; set; }

		public Deserializer(string filePath)
		{
			FileEntity = new FileEntity(filePath);
		}

		public abstract T GetEntity();
	}
}