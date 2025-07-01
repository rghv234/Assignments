import React from "react";
import SkillList from "./components/SkillList";

function App() {
    const skills = [
        "JavaScript",
        "React",
        "Node.js",
        "CSS",
        "HTML",
        "Python",
        "Java",
        "C++",
        "SQL",
        "Git"
    ];

    return (
        <div className="App">
            <h1>My Skills</h1>
            <SkillList skills={skills} />
        </div>
    );
}

export default App;
