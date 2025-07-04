import logo from "./logo.svg";
import "./App.css";
import EmployeeCRUD from "./employeecrud";
import EmployeeDashboard from "./components/EmployeeDashboard";

function App() {
  return (
    <div className="container">
      <EmployeeDashboard />
      {/* <EmployeeCRUD /> */}
    </div>
  );
}

export default App;