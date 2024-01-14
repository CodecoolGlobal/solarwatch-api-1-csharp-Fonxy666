import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Loading from "../Components/Loading/Loading";
import PasswordChange from "../Components/PasswordChange";
import Cookies from "js-cookie";

const updateUserPassword = async (user, token) => {
    try {
        const response = fetch("http://localhost:8080/Auth/Patch", {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify(user)
        })
        const data = await response;
        return data;
    } catch (error) {
        console.error("Error occurred during fetch:", error);
        throw error;
    }
}

const UserPasswordUpdate = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);

    const handleUserPasswordUpdate = (user) => {
        setLoading(true);

        const token = getToken();
        
        updateUserPassword(user, token).then(() => {
            setLoading(false);
            navigate("/");
        })
        .catch((error) => {
            setLoading(false);
            console.error("Error occurred during password change:", error);
        });
        };

    const handleCancel = () => {
        navigate("/");
    };

    const getToken = () => {
        return Cookies.get("jwtToken");
    }

    if (loading) {
        return <Loading />;
    }

    return (
        <PasswordChange
            onSave = { handleUserPasswordUpdate }
            onCancel = { handleCancel }/>
    );
};

export default UserPasswordUpdate;
