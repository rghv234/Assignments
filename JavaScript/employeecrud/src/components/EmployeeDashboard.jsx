import { useMemo, useState, useEffect } from "react";
import { employeeService } from "../services/employeeService";
import DepartmentFilter from "./DepartmentFilter";
import EmployeeTable from "./EmployeeTable";
import EmployeeForm from "./EmployeeForm";
import { wait } from "@testing-library/user-event/dist/utils";

export default function EmployeeDashboard() {
  const [employees, setEmployees] = useState([]);
  const [filterDept, setFilterDept] = useState("All");
  const [error, setError] = useState("");

  useEffect(() => {
    employeeService
      .getAll()
      .then(setEmployees)
      .catch(() => setError("Unable to load the Employees"));
  }, []);

  // useMemo
  const departments = useMemo(
    () => ["All", ...new Set(employees.map((e) => e.department))],
    [employees]
  );

  const filtered = useMemo(
    () =>
      filterDept === "All"
        ? employees
        : employees.filter((e) => e.department === filterDept),
    [employees, filterDept]
  );

  const totalSalary = useMemo(
    () => filtered.reduce((sum, emp) => sum + emp.salary, 0),
    [filtered]
  );

  const handleAdd = async (emp) => {
    try {
      const added = await employeeService.add(emp);
      setEmployees((prev) => [...prev, added]);
    } catch {
      setError("Failed to add new Employee");
    }
  };

  const handleDelete = async (id) => {
    try {
      await employeeService.remove(id);
      setEmployees((prev) => prev.filter((e) => e.id !== id));
    } catch {
      setError("Unable to Delete ");
    }
  };
  console.log(`filterDept ${filterDept} `);
  return (
    <div className="container py-4">
      <h2 className="mb-3">Employee Dashboard</h2>

      {error && <div className="alert alert-danger">{error}</div>}
      <DepartmentFilter
        departments={departments}
        value={filterDept}
        onChange={setFilterDept}
      />

      <EmployeeTable employees={filtered} onDelete={handleDelete} />

      <p className="fw-bold">Total Salary : {totalSalary}</p>

      <EmployeeForm onAdd={handleAdd} />
    </div>
  );
}
