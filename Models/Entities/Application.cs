using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Career.Models.Entities
{
	[Table("Applications")]
	public class Application
	{
		[Key]
		public int id { get; set; }

		public int job_id { get; set; }

		[ForeignKey(nameof(job_id))]
		public Job job { get; set; }

		public int applicant_id { get; set; }

		[ForeignKey(nameof(applicant_id))]
		public User applicant { get; set; }

		public DateTime applied_at { get; private set; }
	}
}