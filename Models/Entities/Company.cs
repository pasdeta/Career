using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Career.Models.Entities
{
	[Table("Companies")]
	public class Company
	{
		[Key]
		public int id { get; set; }

		public string name { get; set; }

		[Column(TypeName = "text")]
		public string address { get; set; }

		public DateTime created_at { get; private set; }

		public DateTime? deleted_at { get; set; }

		public List<Job> jobs { get; set; }
	}
}