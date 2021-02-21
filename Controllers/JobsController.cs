using System;
using System.Collections.Generic;
using System.Linq;
using Career.Contexts;
using Career.Models.DataContracts;
using Career.Models.Entities;
using Career.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Career.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class JobsController : ControllerBase
	{
		private readonly CareerDbContext _db;

		public JobsController(CareerDbContext db)
		{
			this._db = db;
		}

		[HttpGet]
		public ActionResult<IEnumerable<JobViewModel>> List()
		{
			var jobs = _db.Jobs.AsNoTracking()
				.Include(j => j.company)
				.Where(j => j.expire_on >= DateTime.Now && j.is_active == true)
				.OrderBy(j => j.expire_on)
				.Select(j => (JobViewModel) j)
				.ToList();
			
			return Ok(jobs);
		}

		[HttpPost]
		public ActionResult<JobViewModel> Create([FromBody] JobDataContract jobParams)
		{
			Job job = _db.Jobs.Add(jobParams).Entity;
			_db.SaveChanges();
			_db.Entry(job).Reference(j => j.company).Load();

			return Ok((JobViewModel) job);
		}

		[HttpPost("{id:int}/apply")]
		[ProducesResponseType(typeof(JobViewModel), 200)]
		public IActionResult Apply(
			[FromRoute] int id, 
			[FromBody] ApplicationDataContract applicationParams,
			[FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions
		)
		{
			Job job = _db.Jobs.AsNoTracking().Include(j => j.company)
				.Where(j => j.expire_on >= DateTime.Now && j.is_active == true)
				.SingleOrDefault(j => j.id == id);
			if(job == null)
			{

				return NotFound();
			}
			if(_db.Applications.Any(a => a.job_id == id && a.applicant_id == applicationParams.user_id))
			{
				ModelState.AddModelError("user_id", "already applied");

				return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
			}

			_db.Applications.Add(
				new Application {
					applicant_id = applicationParams.user_id,
					job_id = id
				}
			);
			_db.SaveChanges();

			return Ok((JobViewModel) job);
		}
	}
}
