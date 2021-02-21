using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Career.Models.Entities
{
	[Table("Users")]
	public class User
	{
		[Key]
		public int id { get; set; }

		public string username { get; set; }

		public string password { get; set; }

		public string email { get; set; }

		public int? cv_id { get; set; }

		[ForeignKey(nameof(cv_id))]
		public CV cv { get; set; }

		public DateTime created_at { get; private set; }

		public List<Application> applications { get; set; }
	}
}
