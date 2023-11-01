import { useState } from "react";
import Loading from "../Loading/Loading";

const Login = ({ onLogin, user, onCancel }) => {

  const [loading, setLoading] = useState(false);
  const [username, setUsername] = useState(user?.username ?? "");
  const [password, setPassword] = useState(user?.password ?? "");

  const onSubmit = (e) => {
    e.preventDefault();

    return onLogin({
        username,
        password
    });
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <form className="EmployeeForm" onSubmit={onSubmit}>

      <div className="control">
        <label className = "text" htmlFor="username">Username:</label>
        <input
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          name="username"
          id="username"
          className = "text" 
        />
      </div>

      <div className="control">
        <label className = "text" htmlFor="password">Password:</label>
        <input
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          name="password"
          id="password"
          className = "text" 
        />
      </div>

      <div className="buttons">
        <button className = "button" type="submit">
          Login
        </button>
        <button className = "button" type="button" onClick = { onCancel }>
          Cancel
        </button>
      </div>
    </form>
  );
};

export default Login;
