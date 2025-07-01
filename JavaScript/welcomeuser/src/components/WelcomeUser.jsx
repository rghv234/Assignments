import React from 'react';
import '../styles/WelcomeUser.css';

function WelcomeUser({ isLoggedIn, name }) {
    return (
        <div className="welcome-container">
            {isLoggedIn ? (
                <h1>Welcome back, {name}!</h1>
            ) : (
                <h1>Welcome to our website!</h1>
            )}
        </div>
    );
}

export default WelcomeUser;
