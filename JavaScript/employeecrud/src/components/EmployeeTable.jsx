import EmployeeRow from "./EmployeeRow";

export default function EmployeeTable({ employees, onDelete }) {
  return (
    <table className="table table-striped">
      <thead className="table-dark">
        <tr>
          <th>ID</th>
          <th>Name</th>
          <th>Department</th>
          <th>Salary</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {employees.length ? (
          employees.map((emp) => (
            <EmployeeRow key={emp.id} emp={emp} onDelete={onDelete} />
          ))
        ) : (
          <tr>
            <td colSpan="5" className=" text-center text-muted">
              No Employees Found
            </td>
          </tr>
        )}
      </tbody>
    </table>
  );
}
