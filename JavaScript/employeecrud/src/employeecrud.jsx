import React, { useState } from "react";
const initialEmployees = [
  { id: 1, name: "Hariharan", department: "Engineering", salary: 45000 },
  { id: 2, name: "Keerthiswaran", department: "HR", salary: 42000 },
  { id: 3, name: "Lokesh", department: "Finance", salary: 48000 },
  { id: 4, name: "Santo", department: "Engineering", salary: 44000 },
];
export default function EmployeeCRUD() {
  const [employees, setEmployees] = useState(initialEmployees);
  const [formData, setFormData] = useState({
    name: "",
    department: "",
    salary: "",
  });
  const [filterDept, setFilterDept] = useState("All");

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const addEmployee = (e) => {
    e.preventDefault();
    const newEmployee = {
      id: employees.length + 1,
      name: formData.name,
      department: formData.department,
      salary: parseFloat(formData.salary),
    };
    setEmployees([...employees, newEmployee]);
    setFormData({ name: "", department: "", salary: "" });
  };
  const departments = [
    "All",
    ...new Set(initialEmployees.map((emp) => emp.department)),
  ];

  const filteredEmployees =
    filterDept === "All"
      ? employees
      : employees.filter((emp) => emp.department === filterDept);

  const deleteEmployee = (id) => {
    setEmployees(employees.filter((emp) => emp.id !== id));
  };

  const totalSalary = filteredEmployees.reduce(
    (sum, emp) => sum + emp.salary,
    0
  );
  return (
    <>
      <div style={{ padding: "20px" }}>
        <h2>Employee Dashboard</h2>
        <label>Filter by Department</label>
        <select
          value={filterDept}
          onChange={(e) => setFilterDept(e.target.value)}
        >
          {departments.map((dept, idx) => (
            <option key={idx} vlaue={dept}>
              {dept}
            </option>
          ))}
        </select>
        <table className="table">
          <thead className="thead-dark">
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Department</th>
              <th>Salary</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {filteredEmployees.map((emp) => (
              <tr key={emp.id}>
                <td>{emp.id}</td>
                <td>{emp.name}</td>
                <td>{emp.department}</td>
                <td>{emp.salary}</td>
                <td>
                  <button
                    onClick={() => deleteEmployee(emp.id)}
                    className="btn btn-danger"
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>

        <p>
          
          <strong>Total Salary :{totalSalary}</strong>
        </p>
        <h3> Add New Employee</h3>
        <form onSubmit={addEmployee}>
          <input
            type="text"
            name="name"
            placeholder="Name"
            className="form-control"
            value={formData.name}
            onChange={handleChange}
            required
          />
          <br />
          <input
            type="text"
            name="department"
            placeholder="Department"
            className="form-control"
            value={formData.department}
            onChange={handleChange}
            required
          />
          <br />
          <input
            type="number"
            name="salary"
            placeholder="Salary"
            className="form-control"
            value={formData.salary}
            onChange={handleChange}
            required
          />
          <br />
          <button type="submit" className="btn btn-success">
            Add Employee
          </button>
        </form>
      </div>
    </>
  );
}