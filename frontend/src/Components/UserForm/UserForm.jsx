import { useState } from "react";
import Loading from "../Loading/Loading";

const UserForm = ({ onSave, user, onCancel }) => {

  const [loading, setLoading] = useState(false);
  const [email, setEmail] = useState(user?.email ?? "");
  const [username, setUsername] = useState(user?.username ?? "");
  const [password, setPassword] = useState(user?.password ?? "");

  //submit function
  const onSubmit = (e) => {
    e.preventDefault();

    if (user) {
      return onSave({
        ...user,
        email,
        username,
        password
      });
    }

    return onSave({
        email,
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
        <label className="text" htmlFor="email">E-mail:</label>
        <input
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          name="email"
          id="email"
          className="input-field"
        />
      </div>

      <div className="control">
        <label className="text" htmlFor="username">Username:</label>
        <input
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          name="username"
          id="username"
          className="input-field"
        />
      </div>

      <div className="control">
        <label className="text" htmlFor="password">Password:</label>
        <input
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          name="password"
          id="password"
          className="input-field"
        />
      </div>

      <div className="control">
        <label className="text" htmlFor="password">Repeat Password:</label>
        <input
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          name="password"
          id="password"
          className="input-field"
        />
      </div>

      <div className="buttons">
        <button className = "button" type="submit">
          {user ? "Update User" : "Create User"}
        </button>
        <button className = "button" type="button" onClick = { onCancel }>
          Cancel
        </button>
      </div>
    </form>
  );
};

export default UserForm;
