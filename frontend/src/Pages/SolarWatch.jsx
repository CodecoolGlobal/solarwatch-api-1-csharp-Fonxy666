import { useState } from "react";
import { useNavigate } from "react-router-dom";
import SolarWatch from "../Components/MainFunctionality/MainFunctionality";
import Cookies from "js-cookie";
import Loading from "../Components/Loading/Loading";

const GetCountry = async (city, token) => {
  try {
    const response = await fetch(`http://localhost:8080/Sunset-Sunrise/Get?name=${city}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`,
      },
    });

    if (!response.ok) {
      const errorMessage = `HTTP error! Status: ${response.status}`;
      throw new Error(errorMessage);
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error occurred during fetch:", error);
    throw error;
  }
};

const CountryGet = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);

  const handleGet = (city) => {
    setLoading(true);
  
    const token = getToken();
  
    GetCountry(city, token)
      .then((data) => {
        setLoading(false);
        console.log("Response from server:", data);
      })
      .catch((error) => {
        setLoading(false);
        console.error("Error occurred during login:", error);
      });
  };

  const getToken = () => {
    return Cookies.get("jwtToken");
  }

  const handleCancel = () => {
    navigate("/");
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <SolarWatch
      onGet = { handleGet }
      onCancel = { handleCancel }
    />
  );
};

export default CountryGet;



