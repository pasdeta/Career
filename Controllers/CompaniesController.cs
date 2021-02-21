using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Career.Contexts;
using Career.Models.DataContracts;
using Career.Models.Entities;
using Career.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Career.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CompaniesController : ControllerBase
	{
		private readonly CareerDbContext _db;
		
		public CompaniesController(CareerDbContext db)
		{
			this._db = db;
		}

		[HttpGet]
		public ActionResult<IEnumerable<CompanyViewModel>> List()
		{
			var companies = _db.Companies.AsNoTracking().OrderBy(c => c.name).Select(c => (CompanyViewModel) c);

			return Ok(companies);
		}

		[HttpGet("{id:int}")]
		public ActionResult<CompanyViewModel> Get([FromRoute] int id)
		{
			var company = _db.Companies.AsNoTracking()
				.Where(c => c.id == id)
				.Select(c => (CompanyViewModel) c)
				.SingleOrDefault();
				
			if(company == null)
			{

				return NotFound();
			}

			return Ok(company);
		}

		[HttpPost]
		public ActionResult<CompanyViewModel> Create([FromBody] CompanyDataContract companyParams)
		{
			Company company = _db.Companies.Add(companyParams).Entity;
			_db.SaveChanges();

			return Ok((CompanyViewModel) company);
		}

		[HttpPut("{id:int}")]
		public ActionResult<CompanyViewModel> Update([FromRoute] int id, CompanyDataContract companyParams)
		{
			Company company = _db.Companies.SingleOrDefault(c => c.id == id);
			if(company == null)
			{

				return NotFound();
			}
			company.name = companyParams.name;
			company.address = companyParams.address;
			_db.SaveChanges();

			return Ok((CompanyViewModel) company);
		}

		[HttpDelete("{id:int}")]
		public IActionResult Delete([FromRoute] int id)
		{
			Company company = _db.Companies.SingleOrDefault(c => c.id == id);
			if(company == null)
			{

				return NotFound();
			}
			company.deleted_at = DateTime.Now;
			_db.SaveChanges();

			return Ok();
		}


		[HttpGet("{id:int}/applications")]
		public ActionResult<IEnumerable<JobWithApplicationsViewModel>> Applications([FromRoute] int id)
		{
			var company = _db.Companies.AsNoTracking()
				.Include(c => c.jobs.Where(j => j.is_active == true))
				.ThenInclude(j => j.applications)
				.ThenInclude(a => a.applicant)
				.ThenInclude(u => u.cv)
				.AsSplitQuery()
				.Where(c => c.id == id)
				.SingleOrDefault();
			
			if(company == null)
			{

				return NotFound();
			}

			var applications = company.jobs.Select(j => (JobWithApplicationsViewModel) j);

			return Ok(applications);
		}


		[HttpGet("{id:int}/jobs")]
		public ActionResult<IEnumerable<JobViewModel>> Jobs([FromRoute] int id)
		{
			var company = _db.Companies.AsNoTracking()
				.Include(c => c.jobs)
				.Where(c => c.id == id)
				.SingleOrDefault();
			
			if(company == null)
			{

				return NotFound();
			}

			var jobs = company.jobs.Select(j => (JobViewModel) j);

			return Ok(jobs);
		}
	}
}
