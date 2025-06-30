import React from 'react';
import LoginForm from './components/LoginFormik'; // or './LoginForm' if not in folder

function App() {
  const handleLogin = (username, password) => {
    if (username === 'admin' && password === '1234') {
      alert('Login successful!');
      return true;
    }
    return false;
  };

  return (
    <div className="container d-flex justify-content-center align-items-center" style={{ minHeight: '100vh' }}>
      <LoginForm onLogin={handleLogin} />
    </div>
  );
}

export default App;
