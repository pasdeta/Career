using System;
using System.Collections.Generic;
using System.Linq;
using Career.Models.Entities;

namespace Career.Models.ViewModels
{
	public class JobWithApplicationsViewModel : JobViewModel
	{
		public List<UserWithCVViewModel> applicants { get; set; }

		public static implicit operator JobWithApplicationsViewModel(Job job)
		{

			return new JobWithApplicationsViewModel {
				id = job.id,
				description = job.description,
				location = job.location,
				expire_on = job.expire_on,
				applicants = job.applications.Select(a => (UserWithCVViewModel) a.applicant).ToList()
			};
		}
	}
}