import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Login from "../Components/Login/Login";
import Cookies from "js-cookie";

const postLogin = (user) => {
    return fetch("http://localhost:5120/Auth/Login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    }).then((res) => res.json());
  };

const UserLogin = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);

  const handleOnLogin = (user) => {
    setLoading(true);

    postLogin(user)
      .then((data) => {
        setLoading(false);
        const expires = new Date(new Date().getTime() + 60000);
        Cookies.set("jwtToken", data.token, { expires });
        navigate("/");
      })
    };

    const handleCancel = () => {
        navigate("/");
    };

  return (
    <Login
      onLogin = { handleOnLogin }
      onCancel = { handleCancel }
    />
  );
};

export default UserLogin;
