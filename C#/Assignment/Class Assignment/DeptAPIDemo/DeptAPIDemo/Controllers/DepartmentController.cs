using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeptAPIDemo.Services;
using DeptAPIDemo.Models;

namespace DeptAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService; 
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null) return NotFound();
            return Ok(department);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            if (department == null) return BadRequest("Department cannot be null");
            var createdDepartment = await _departmentService.CreateDepartmentAsync(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.DepartmentId }, createdDepartment);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department)
        {
            if (department == null) return BadRequest("Department cannot be null");
            var updatedDepartment = await _departmentService.UpdateDepartmentAsync(id, department);
            if (updatedDepartment == null) return NotFound();
            return Ok(updatedDepartment);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _departmentService.DeleteDepartmentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
