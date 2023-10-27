import { Outlet, Link, useLocation } from "react-router-dom";
import "./Layout.css";

const Layout = () => {
  const location = useLocation();

  return (
    <div className="Layout">
      <nav>
        <ul>
          <li className="grow">
            Solarwatch
          </li>
          <li>
            <Link to="/reg">
              <button type="button">Registration</button>
            </Link>
          </li>
        </ul>
      </nav>
      <Outlet />
    </div>
  );
};

export default Layout;