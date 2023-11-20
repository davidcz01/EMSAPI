using EmsAPIV2.Data;
using EmsAPIV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmsAPIV2.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {




        private readonly EmsAPIV2Context _context;

        public EmployeeController(EmsAPIV2Context context)
        {
            _context = context;

        }



        private static readonly List<FakeData> data = new()
        {
            new FakeData { Name = "One", Description = "Data 1" },
            new FakeData { Name = "Two", Description = "Data 2" },
        };


        [HttpGet("api/Employee/GetFakeData")]
        public IActionResult GetFakeData()
        {
            return Ok(data);
        }


        [HttpGet("api/Employee/GetEmployeeData")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var employees = await _context.Employees.ToListAsync();

                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Exception: {ex}");

                // Return a more informative error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpPost("api/Employee/AddEmployee")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployees), new { id = employee.EmployeeID }, employee);



        }


        [HttpDelete("api/Employee/DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromQuery] int id)
        {
            var selectedEmployee = await _context.Employees.FindAsync(id);

            if (selectedEmployee == null)
            {
                return NotFound($"Employee with ID {id} not found");
            }

            _context.Employees.Remove(selectedEmployee);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content
        }



        [HttpPut("api/Employee/UpdateEmployee/{id}")]
        public IActionResult Update(int id, [FromBody] Employee updatedEmployee)
        {
            var existingemployee = _context.Employees.FirstOrDefault(e => e.EmployeeID == id);

            if (existingemployee == null)
            {
                return NotFound();
            }

            existingemployee.FirstName = updatedEmployee.FirstName;
            existingemployee.Surname = updatedEmployee.Surname;
            existingemployee.DepartmentName = updatedEmployee.DepartmentName;
            existingemployee.Date = updatedEmployee.Date;
            existingemployee.Email = updatedEmployee.Email;
            existingemployee.DepartmentID = updatedEmployee.DepartmentID;
            existingemployee.HolidayEntitlement = updatedEmployee.HolidayEntitlement;

            _context.SaveChangesAsync(); 

            return Ok(existingemployee);
        }




    }
}





