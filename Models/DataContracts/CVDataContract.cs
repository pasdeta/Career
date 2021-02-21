using System.ComponentModel.DataAnnotations;
using Career.Models.Entities;

namespace Career.Models.DataContracts
{
	public class CVDataContract
	{
		[Required]
		public string education { get; set; }

		[Range(0, 100)]
		public int experience { get; set; }

		[Required]
		public string work_experience { get; set; }

		public static implicit operator CV(CVDataContract cv)
		{

			return new CV {
				education = cv.education,
				experience = cv.experience,
				work_experience = cv.work_experience
			};
		}
	}
}