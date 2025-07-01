import React from "react";
import ProfileCard from "./components/ProfileCard";

function App() {
  return (
    <div>
      <h1>Employee Directory</h1>
      <ProfileCard 
        name="Raghav Govindarajan"
        title="Software Engineer"
        image="https://via.placeholder.com/100"
      />
    </div>
  );
}

export default App;
