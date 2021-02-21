using System;
using System.Text.Json.Serialization;
using Career.Models.Entities;

namespace Career.Models.ViewModels
{
	public class JobViewModel
	{
		public int id { get; set; }

		public string title { get; set; }

		public string description { get; set; }

		public string location { get; set; }

		public DateTime expire_on { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public CompanyViewModel company { get; set; }

		public static implicit operator JobViewModel(Job job)
		{

			return new JobViewModel {
				id = job.id,
				title = job.title,
				description = job.description,
				location = job.location,
				expire_on = job.expire_on,
				company = job.company
			};
		}
	}
}