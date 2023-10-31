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
          <Link to="/reg">
            <button className = "button" type="button">Registration</button>
          </Link>
          {jwtToken ? (
            <div>
              <Link to="/solar-watch">
                <button className = "button" type="button">
                  Solar-watch
                </button>
              </Link>
              <button className = "button" type="button" onClick = { handleLogout }>
                Logout
              </button>
            </div>
          ) : (
            <div>
              <Link to="/login">
                <button className = "button" type="button">Login</button>
              </Link>
            </div>
          )}
        </ul>
      </nav>
      <Outlet />
    </div>
  );
};

export default Layout;