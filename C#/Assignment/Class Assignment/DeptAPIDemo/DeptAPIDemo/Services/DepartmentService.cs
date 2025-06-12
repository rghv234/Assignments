using DeptAPIDemo.Models;
namespace DeptAPIDemo.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly List<Department> _departments = new List<Department>
        {
            new Department { DepartmentId = 1, Name = "HR", Location = "Building A" },
            new Department { DepartmentId = 2, Name = "IT", Location = "Building B" }
        };
        public Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return Task.FromResult<IEnumerable<Department>>(_departments);
        }
        public Task<Department?> GetDepartmentByIdAsync(int id)
        {
            var department = _departments.FirstOrDefault(d => d.DepartmentId == id);
            return Task.FromResult(_departments.FirstOrDefault(d => d.DepartmentId == id));
        }
        public Task<Department> CreateDepartmentAsync(Department department)
        {
            department.DepartmentId = _departments.Max(d => d.DepartmentId) + 1;
            _departments.Add(department);
            return Task.FromResult(department);
        }
        public Task<Department?> UpdateDepartmentAsync(int id, Department department)
        {
            var existingDepartment = _departments.FirstOrDefault(d => d.DepartmentId == id);
            if (existingDepartment == null) return Task.FromResult<Department?>(null);

            existingDepartment.Name = department.Name;
            existingDepartment.Location = department.Location;
            return Task.FromResult(existingDepartment);
        }
        public Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = _departments.FirstOrDefault(d => d.DepartmentId == id);
            if (department == null) return Task.FromResult(false);

            _departments.Remove(department);
            return Task.FromResult(true);
        }
    }
}
