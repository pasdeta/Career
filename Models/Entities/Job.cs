using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Career.Models.Entities
{
	[Table("Jobs")]
	public class Job
	{
		[Key]
		public int id { get; set; }

		public int company_id { get; set; }

		[ForeignKey(nameof(company_id))]
		public Company company { get; set; }

		public string title { get; set; }

		[Column(TypeName = "text")]
		public string description { get; set; }

		public string location { get; set; }

		public DateTime created_at { get; private set; }

		public DateTime expire_on { get; set; }

		public bool? is_active { get; private set; }

		public List<Application> applications { get; set; }
	}
}