import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider, Navigate } from "react-router-dom";
import reportWebVitals from "./reportWebVitals";
import Cookies from "js-cookie";

import Layout from "./Pages/Layout";
import UserCreator from "./Pages/UserCreator";
import Login from "./Pages/Login";
import SolarWatch from "./Pages/SolarWatch";
import PasswordChange from "./Pages/PasswordChange";

const App = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  
  const checkIfUserIsAuthenticated = () => {
    return Cookies.get("jwtToken");
  };
  
  useEffect(() => {
    const jwtToken = checkIfUserIsAuthenticated();
    setIsAuthenticated(!isAuthenticated);
  }, []);

  const router = createBrowserRouter([
    {
      path: '/',
      element: <Layout />,
      children: [
        {
          path: '/',
          element: <div className="welcome-text">Welcome to the page!</div>,
        },
        {
          path: '/reg',
          element: <UserCreator />,
        },
        {
          path: '/account/login',
          element: isAuthenticated ? <Login /> : <div className="welcome-text">You are already logged in!</div>
        },
        {
          path: '/account/change-password',
          element: isAuthenticated ? <Login /> : <div className="welcome-text">Log in first to change password! :)</div>
        },
        {
          path: '/solar-watch',
          element: isAuthenticated ? <SolarWatch /> : <Navigate to="/account/login" />,
        },
        {
          path: '/city/:cityName',
          element: <PasswordChange />
        },
        {
          path: '/user/password-change',
          element: <PasswordChange />
        }
      ],
    },
  ]);

  return (
    <React.StrictMode>
      <RouterProvider router={router}>
      </RouterProvider>
    </React.StrictMode>
  );
};

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(<App />);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
