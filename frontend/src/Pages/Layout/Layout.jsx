import React, { useState, useEffect } from "react";
import { Outlet, Link, useLocation } from "react-router-dom";
import Cookies from "js-cookie";
import "./Layout.css";

const Layout = () => {
  const location = useLocation();
  const [jwtToken, setJwtToken] = useState(Cookies.get("jwtToken"));

  useEffect(() => {
    const token = Cookies.get("jwtToken");
    setJwtToken(token);
  }, [location]);

  const handleLogout = () => {
    Cookies.remove("jwtToken");
    setJwtToken(null);
  };

  return (
    <div className="Layout">
      <nav>
        <ul>
          <li className="grow">Solarwatch</li>
            <Link to="/reg">
              <button type="button">Registration</button>
            </Link>
          {jwtToken ? (
            <li>
              <Link to="/solar-watch">
                <button type="button">
                  Solar-watch
                </button>
              </Link>
              <button type="button" onClick = { handleLogout }>
                Logout
              </button>
            </li>
          ) : (
            <li>
              <Link to="/login">
                <button type="button">Login</button>
              </Link>
            </li>
          )}
        </ul>
      </nav>
      <Outlet />
    </div>
  );
};

export default Layout;