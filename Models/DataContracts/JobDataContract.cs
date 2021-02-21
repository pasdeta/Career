using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Career.Contexts;
using Career.Models.Entities;

namespace Career.Models.DataContracts
{
	public class JobDataContract : IValidatableObject
	{
		public int company_id { get; set; }

		[Required]
		public string title { get; set; }

		[Required]
		public string description { get; set; }

		[Required]
		public string location { get; set; }

		[Required]
		public DateTime expire_on { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if((expire_on - DateTime.Now) < TimeSpan.FromDays(2))
			{

				yield return new ValidationResult("atleast need 2 days", new[] { nameof(expire_on) });
			}

			var db = validationContext.GetService(typeof(CareerDbContext)) as CareerDbContext;
			if(db.Companies.Any(c => c.id == company_id) == false)
			{

				yield return new ValidationResult("unknown company", new[] { nameof(company_id) });	
			}
		}

		public static implicit operator Job(JobDataContract job)
		{

			return new Job {
				company_id = job.company_id,
				title = job.title,
				description = job.description,
				location = job.location,
				expire_on = job.expire_on
			};
		}
	}
}

