import React from "react";
import '../styles/SkillList.css';

function SkillList({ skills }) {
    return (
        <div className="skill-list-container">
            <h2>Skills</h2>
            <ul className="skill-list">
                {skills.map((skill, index) => (
                    <li key={index} className="skill-item">
                        {skill}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default SkillList;
