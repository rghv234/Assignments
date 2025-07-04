let _employees = [
  { id: 1, name: "Hariharan", department: "Engineering", salary: 45000 },
  { id: 2, name: "Keerthiswaran", department: "HR", salary: 42000 },
  { id: 3, name: "Lokesh", department: "Finance", salary: 48000 },
  { id: 4, name: "Santo", department: "Engineering", salary: 44000 },
];

export const employeeService = {
  getAll() {
    return Promise.resolve([..._employees]);
  },

  add(emp) {
    const newEmp = { ...emp, id: _employees.length + 1 };
    _employees.push(newEmp);
    return newEmp;
  },

  remove(id) {
    const _initialLength = _employees.length;
    _employees = _employees.filter((e) => e.id == id);
    return _employees.length !== _initialLength;
  },
};