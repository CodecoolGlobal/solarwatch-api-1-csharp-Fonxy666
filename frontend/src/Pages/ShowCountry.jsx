import React, { useState, useEffect } from 'react';
import GetImage from '../Components/GetImageForCountry';
import { useNavigate } from 'react-router-dom';

const GetCountry = async (apiKey) => {
    try {
        const url = new URL(window.location.href);
        const cityName = url.pathname.split('/').pop();
        const response = await fetch(`https://api.pexels.com/v1/search?query=${cityName}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `${apiKey}`
            },
        });
        if (!response.ok) {
            const errorMessage = `HTTP error! Status: ${response.status}`;
            throw new Error(errorMessage);
        }
        const data = await response.json();
        return data.photos[3].src.large;
    } catch (error) {
        console.error("Error occurred during fetch:", error);
        throw error;
    }
};

const ShowCountry = () => {
    const [imageUrl, setImageUrl] = useState(null);
    const navigate = useNavigate();

    const handleGet = async () => {
        const apiKey = process.env.REACT_APP_IMAGE_API_KEY;
        try {
            const data = await GetCountry(apiKey);
            setImageUrl(data);
        } catch (error) {
            alert(`There is no City: in our database!`);
            console.error("Error occurred during login:", error);
        }
    };

    const handleCancel = () => {
        navigate("/");
    };

    useEffect(() => {
        handleGet();
    }, []);

    return (
        <GetImage
            city = { imageUrl }
            onCancel = { handleCancel }/>
    )
}

export default ShowCountry