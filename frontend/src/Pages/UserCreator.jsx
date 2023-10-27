import { useState } from "react";
import { useNavigate } from "react-router-dom";
import UserForm from "../Components/UserForm";

const createUser = (user) => {
    return fetch("http://localhost:5120/Auth/Register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    }).then((res) => res.json());
  };

const UserCreator = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);

  const handleCreateUser = (user) => {
    setLoading(true);

    createUser(user)
      .then(() => {
        setLoading(false);
        navigate("/");
      })
    };

    const handleCancel = () => {
        console.log("haha");
        navigate("/");
    };

  return (
    <UserForm
        onSave = { handleCreateUser }
        onCancel = { handleCancel }
    />
  );
};

export default UserCreator;
