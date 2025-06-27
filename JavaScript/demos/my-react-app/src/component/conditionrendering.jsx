import React from 'react';

function ConditionRendering() {
  const isLoggedIn = true; // Change this to false to see the alternate message
  const error = null; // Simulate no error. Replace with an actual error object to test
  const employees = []; // Example employee list
  const user = { role: 'admin' }; // Example user object

  return (
    <div>
      {/* Error Handling */}
      {error ? (
        <p style={{ color: 'red', backgroundColor: 'white', fontSize: '20px' }}>
          <strong>Error: </strong>
          {error.message}
        </p>
      ) : (
        <>
          {/* Conditional greeting or login button */}
          {isLoggedIn ? (
            <p>Welcome Back!</p>
          ) : (
            <button>Login</button>
          )}

          {/* Employee List */}
          {employees.length === 0 ? (
            <p>No Employees Found</p>
          ) : (
            <ul>
              {employees.map((emp, index) => (
                <li key={index}>{emp}</li>
              ))}
            </ul>
          )}

          {/* Role-based UI */}
          <div>
            {user.role === 'admin' ? (
              <button>Manage Customers</button>
            ) : (
              <p>You have Readonly access</p>
            )}
          </div>
        </>
      )}
    </div>
  );
}

export default ConditionRendering;
