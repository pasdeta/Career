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
	[Route("/Users/{id:int}/[controller]")]
	public class CVController : ControllerBase
	{
		private readonly CareerDbContext _db;

		public CVController(CareerDbContext db)
		{
			this._db = db;
		}

		[HttpGet]
		public ActionResult<CVViewModel> Get([FromRoute] int id)
		{
			CVViewModel cv = _db.Users.AsNoTracking().Include(u => u.cv)
				.Where(u => u.id == id && u.cv_id.HasValue)
				.Select(u => (CVViewModel) u.cv)
				.SingleOrDefault();

			if(cv == null)
			{

				return NotFound();
			}

			return Ok(cv);
		}

		[HttpPost]
		public ActionResult Create(
			[FromRoute] int id,
			[FromBody] CVDataContract cvParams
		)
		{
			User user = _db.Users.Include(u => u.cv).SingleOrDefault(u => u.id == id);
			if(user == null)
			{

				return NotFound();
			}
			
			if(user.cv != null)
			{
				_db.Remove(user.cv);
			}
			user.cv = cvParams;
			
			_db.SaveChanges();

			return Ok((CVViewModel) user.cv);
		}
	}
}
