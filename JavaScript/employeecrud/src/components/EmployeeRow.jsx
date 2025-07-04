export default function EmployeeRow({ emp, onDelete }) {
  return (
    <tr>
      <td>{emp.id}</td>
      <td>{emp.name}</td>
      <td>{emp.department}</td>
      <td>{emp.salary}</td>
      <td>
        <button onClick={() => onDelete(emp.id)} className="btn btn-danger">
          Delete
        </button>
      </td>
    </tr>
  );
}

