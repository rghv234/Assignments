﻿using DeptAPIDemo.Models;
namespace DeptAPIDemo.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department?> GetDepartmentByIdAsync(int id);
        Task<Department> CreateDepartmentAsync(Department department);
        Task<Department?> UpdateDepartmentAsync(int id, Department department);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
