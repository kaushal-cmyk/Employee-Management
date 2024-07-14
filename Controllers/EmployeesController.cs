using Microsoft.AspNetCore.Mvc;

namespace CrudOperation.Controllers
{
	public class EmployeesController : Controller
	{
		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}



	}
}
