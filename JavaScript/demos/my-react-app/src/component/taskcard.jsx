import React from "react";
import "./taskcard.css";
export default function TaskCard({ task }) {
    const cardStyle = {
        border: "1px solid #ccc",
        borderRadius: "8px",
        padding: "16px",
        margin: "16px 0",
        backgroundColor: "#f9f9f9"
    };
    const statusColor = {
        Pending: "red",
        "In progress": "orange",
        Completed: "green"
    };

    const statusClass = task.status.toLowerCase().replace(" ", "-");    
    return (
        <div className="taskContainer">
            <div className={'taskCard ${statusClass}'} style={cardStyle}>
                <h3>{task.title}</h3>
                <p>{task.description}</p>
               <p
               style={{
                    color: statusColor[task.status] || "black",
                    fontWeight: "bold"
                }}>
                    {task.status}
               </p>
            </div>
        </div>
    )
}