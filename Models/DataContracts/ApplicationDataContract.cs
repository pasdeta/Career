using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Career.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Career.Models.DataContracts
{
	public class ApplicationDataContract : IValidatableObject
	{
		[Required]
		[Range(1, int.MaxValue)]
		public int user_id { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var db = validationContext.GetService(typeof(CareerDbContext)) as CareerDbContext;

			var user = db.Users.AsNoTracking().Where(u => u.id == user_id).Select(u => new { has_cv = u.cv_id.HasValue }).SingleOrDefault();
			if(user == null)
			{

				yield return new ValidationResult("unknown user", new[] { nameof(user_id) });	
			}
			else if(user.has_cv == false)
			{

				yield return new ValidationResult("user has no CV", new[] { nameof(user_id) });	
			}
		}
	}

}