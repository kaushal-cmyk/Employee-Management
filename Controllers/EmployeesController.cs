using CrudOperation.Data;
using CrudOperation.Models;
using CrudOperation.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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

		[HttpGet]
		public async Task<IActionResult> View(Guid id)
		{
			var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

			if (employee != null)
			{
				var viewModel = new UpdateEmployeeViewModel()
				{
					Id = employee.Id,
					Name = employee.Name,
					Email = employee.Email,
					Salary = employee.Salary,
					DateOfBirth = employee.DateOfBirth,
					Department = employee.Department,
				};
				return await Task.Run(() => View("View", viewModel));
			}
			return RedirectToAction("Index");
		}


		[HttpPost]

		public async Task<IActionResult> View(UpdateEmployeeViewModel updateEmployee)
		{
			var employee = await mvcDemoDbContext.Employees.FindAsync(updateEmployee.Id);

			if (employee != null)
			{
				employee.Name = updateEmployee.Name;
				employee.Email = updateEmployee.Email;
				employee.Salary = updateEmployee.Salary;
				employee.DateOfBirth = updateEmployee.DateOfBirth;
				employee.Department = updateEmployee.Department;

				await mvcDemoDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(UpdateEmployeeViewModel updateEmployee)
		{
			var employee = await mvcDemoDbContext.Employees.FindAsync(updateEmployee.Id);

			if (employee != null)
			{
				mvcDemoDbContext.Employees.Remove(employee);
				await mvcDemoDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}
	}
}
