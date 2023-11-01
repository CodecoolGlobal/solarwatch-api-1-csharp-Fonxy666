import React, { useState, useEffect } from "react";
import { Outlet, Link, useLocation, useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import "./Layout.css";

const Layout = () => {
  const location = useLocation();
  const [jwtToken, setJwtToken] = useState(Cookies.get("jwtToken"));
  const navigate = useNavigate();

  useEffect(() => {
    const token = Cookies.get("jwtToken");
    setJwtToken(token);
  }, [location]);

  const handleLogout = () => {
    Cookies.remove("jwtToken");
    navigate("/");
    setJwtToken(null);
  };

  return (
    <div className="Layout">
      <nav>
        <ul>
          {!jwtToken ? (
            <div>
              <Link to="/login">
                <button className = "button" type="button">Login</button>
              </Link>
              <Link to="/reg">
                <button className = "button" type="button">Registration</button>
              </Link>
            </div>
          ) : (
            <div>
              <button className = "button" type="button" onClick = { handleLogout }>Logout</button>
              <Link to="/solar-watch">
                <button className = "button" type="button">Solar-watch</button>
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