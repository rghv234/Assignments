import react from 'react';
import { useState } from 'react';

function Home() {
    return (
        <div>
            <header>
                <h1>Welcome to My React App</h1>
                <p>This is the home page of your React application.</p>
            </header>

            <nav>
                <ul>
                    <li>
                        <a href='#'>Home</a>    
                    </li>
                    <li>
                        <a href='#'>About</a>
                    </li>
                    <li>
                        <a href='#'>Services</a>
                    </li>
                    <li>
                        <a href='#'>Contact</a>
                    </li>
                </ul>
            </nav>
        </div>
    );
}