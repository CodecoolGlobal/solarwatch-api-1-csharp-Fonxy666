import { useState } from "react";
import Loading from "../Loading/Loading";

const SolarWatch = ({ onGet, country, onCancel }) => {

  const [loading, setLoading] = useState(false);
  const [getName, setGetName] = useState(country?.name ?? "");
  const [postCountryName, setostCountryName] = useState(country?.name ?? "");

  const onSubmit = (e) => {
    e.preventDefault();

    return onGet(
      getName
    );
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <form className="EmployeeForm" onSubmit={onSubmit}>
      
      <div className="control">
        <label className = "text" htmlFor="name">Get:</label>
        <input
          value={getName}
          onChange={(e) => setGetName(e.target.value)}
          name="name"
          id="name"
          className="input-field"
        />
      </div>

      <div className="buttons">
        <button className = "button" type="submit" >
          Get country
        </button>
        <button className = "button" type="button" onClick = { onCancel }>
          Cancel
        </button>
      </div>
      
      <div className="control">
        <label className = "text" htmlFor="name">Post:</label>
        <input
          value={postCountryName}
          onChange={(e) => setostCountryName(e.target.value)}
          name="name"
          id="name"
          className="input-field"
        />
      </div>

      <div className="buttons">
        <button className = "button" type="submit">
          Post country
        </button>
        <button className = "button" type="button" onClick = { onCancel }>
          Cancel
        </button>
      </div>

    </form>
  );
};

export default SolarWatch;
