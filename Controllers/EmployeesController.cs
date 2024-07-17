using CrudOperation.Data;
using CrudOperation.Models;
using CrudOperation.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly MVCDemoDbContext mvcDemoDbContext;

		public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
		{
			this.mvcDemoDbContext = mvcDemoDbContext;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var employees = await mvcDemoDbContext.Employees.ToListAsync();
			return View(employees);
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
		{
			var employee = new Employee()
			{
				Id = Guid.NewGuid(),
				Name = addEmployeeRequest.Name,
				Email = addEmployeeRequest.Email,
				Salary = addEmployeeRequest.Salary,
				DateOfBirth = addEmployeeRequest.DateOfBirth,
				Department = addEmployeeRequest.Department,
			};

			await mvcDemoDbContext.Employees.AddAsync(employee);
			await mvcDemoDbContext.SaveChangesAsync();
			return RedirectToAction("Index");

		}

	}
}
