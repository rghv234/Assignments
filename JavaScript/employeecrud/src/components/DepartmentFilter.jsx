export default function DepartmentFilter({ departments, value, onChange }) {
  return (
    <div className="mb-3">
      <label className="form-label">Filter by Department</label>
      <select
        className="form-select"
        value={value}
        onChange={(e) => onChange(e.target.value)}
      >
        {departments.map((dept, idx) => (
          <option key={idx} value={dept}>
            {dept}
          </option>
        ))}
      </select>
    </div>
  );
}
