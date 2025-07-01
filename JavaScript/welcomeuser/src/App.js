import React from "react";
import WelcomeUser from "./components/WelcomeUser";

function App() {
  const isLoggedIn = true;
  const userName = "Raghav";

  return (
    <div className="App">
      <WelcomeUser isLoggedIn={isLoggedIn} name={userName} />
    </div>
  );
}

export default App;
