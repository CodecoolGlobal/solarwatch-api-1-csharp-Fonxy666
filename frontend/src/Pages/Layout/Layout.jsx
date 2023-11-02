import React, { useState, useEffect } from "react";
import { Outlet, Link, useLocation, useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import { ButtonContainer } from "../../Components/Styles/Buttoncontainer.styled";
import { ButtonRowContainer } from "../../Components/Styles/ButtonRow.styled";

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
        {!jwtToken ? (
          <ButtonRowContainer>
            {location.pathname !== "/login" && (
              <Link to="/login">
                <ButtonContainer type="button">Login</ButtonContainer>
              </Link>
            )}
            <Link to="/reg">
              <ButtonContainer type="button">Registration</ButtonContainer>
            </Link>
          </ButtonRowContainer>
        ) : (
          <ButtonRowContainer>
            <button className = "button" type="button" onClick = { handleLogout }>Logout</button>
            <Link to="/solar-watch">
              <button className = "button" type="button">Solar-watch</button>
            </Link>
          </ButtonRowContainer>
          )}
      </nav>
      <Outlet />
    </div>
  );
};

export default Layout;