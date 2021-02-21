using System.Collections.Generic;
using System.Linq;
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
	public class UsersController : ControllerBase
	{
		private readonly CareerDbContext _db;

		public UsersController(CareerDbContext db)
		{
			this._db = db;
		}

		[HttpGet]
		public ActionResult<IEnumerable<UserViewModel>> List()
		{
			var users = _db.Users.AsNoTracking().OrderBy(u => u.id).Select(u => (UserViewModel) u);

			return Ok(users);
		}

		[HttpPost]
		public ActionResult Create([FromBody] UserDataContract userParams)
		{
			User user = _db.Users.Add(userParams).Entity;
			_db.SaveChanges();

			return Ok();
		}

		[HttpGet("{id:int}")]
		public ActionResult<UserViewModel> Profile([FromRoute] int id)
		{
			UserViewModel user = _db.Users.AsNoTracking()
				.Where(u => u.id == id)
				.Select(u => (UserViewModel) u)
				.SingleOrDefault();
			if(user == null)
			{

				return NotFound();
			}

			return Ok(user);
		}

		[HttpGet("{id:int}/applications")]
		public ActionResult<IEnumerable<UserApplicationViewModel>> Applications([FromRoute] int id)
		{
			if(_db.Users.Any(u => u.id == id) == false)
			{

				return NotFound();
			}

			var applications = _db.Applications.AsNoTracking()
							.Include(a => a.job)
							.ThenInclude(j => j.company)
							.Where(a => a.applicant_id == id && a.job.is_active == true)
							.Select(a => (UserApplicationViewModel) a);

							
			return Ok(applications);
		}
		

		[HttpDelete("{id:int}")]
		public IActionResult Delete([FromRoute] int id)
		{
			User user = _db.Users.SingleOrDefault(u => u.id == id);
			if(user == null)
			{

				return NotFound();
			}
			_db.Remove(user);
			_db.SaveChanges();

			return Ok();
		}
	}
}
