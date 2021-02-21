using System;
using Career.Models.Entities;

namespace Career.Models.ViewModels
{
	public class CVViewModel
	{
		public string education { get; set; }

		public int experience { get; set; }

		public string work_experience { get; set; }

		public static implicit operator CVViewModel(CV cv)
		{

			return new CVViewModel {
				education = cv.education,
				experience = cv.experience,
				work_experience = cv.work_experience
			};
		}
	}
}