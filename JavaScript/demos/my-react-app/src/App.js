import logo from './logo.svg';
import './App.css';

function App() {
 let name = 'World';
 let age = 25;
 let isStudent = true;
 let studentNames = ['John', 'Jane', 'Doe'];
 let studentInfo = {
   name: 'Alice',
   age: 22,
   isEnrolled: true
 };
const tasks = [
   { title: "todo task", description: 'update status', status: "In progress" },
   { title: 'frontend development', description: 'update status', status: "Completed" },
   { title: 'backend development', description: 'update status', status: "Pending" }
 ];
 return (
  <div>
    {tasks.map((task, index) => (
      <TaskCard key={index} task={task} />
    ))}
  </div>
 );
}

export default App;
