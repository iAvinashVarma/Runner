using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ExcelController.Model
{
	[Serializable()]
	[XmlType(AnonymousType = true)]
	[XmlRoot("employees", Namespace = "", IsNullable = false)]
	public partial class Employees
	{

		[XmlElement("employee")]
		public List<Employee> Employee { get; set; }
	}

	/// <remarks/>
	[Serializable()]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	public partial class Employee
	{
		[XmlElement("firstname")]
		public string FirstName { get; set; }

		[XmlElement("lastname")]
		public string LastName { get; set; }

		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement("division")]
		public string Division { get; set; }

		[XmlElement("building")]
		public ushort Building { get; set; }

		[XmlElement("room")]
		public string Room { get; set; }

		[XmlElement("supervisor")]
		public string Supervisor { get; set; }

		[XmlAttribute("id")]
		public string Id { get; set; }
	}


}
