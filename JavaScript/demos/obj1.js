const employees = [
  { id: 1, name: 'Alice', department: 'HR' },
    { id: 2, name: 'Bob', department: 'Engineering' },
    { id: 3, name: 'Charlie', department: 'Marketing' },
    { id: 4, name: 'David', department: 'Engineering' },
    { id: 5, name: 'Eve', department: 'HR' }
];
const updatedEmployees = employees.map(employee => {
    if (employee.department === 'Engineering') {
        return { ...employee, department: 'Tech' };
    }
    return employee;
});
console.log("Updated Employees:", updatedEmployees);
const engineeringEmployees = employees.filter(employee => employee.department === 'Engineering');
console.log("Engineering Employees:", engineeringEmployees);