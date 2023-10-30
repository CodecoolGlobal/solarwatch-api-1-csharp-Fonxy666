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
        <label htmlFor="username">Username:</label>
        <input
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          name="username"
          id="username"
        />
      </div>

      <div className="control">
        <label htmlFor="password">Password:</label>
        <input
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          name="password"
          id="password"
        />
      </div>

      <div className="buttons">
        <button type="submit">
          Login
        </button>
        <button type="button" onClick = { onCancel }>
          Cancel
        </button>
      </div>
    </form>
  );
};

export default Login;
