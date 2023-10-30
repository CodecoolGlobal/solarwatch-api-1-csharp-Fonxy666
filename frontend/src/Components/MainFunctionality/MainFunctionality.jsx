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
        <label htmlFor="name">Get:</label>
        <input
          value={getName}
          onChange={(e) => setGetName(e.target.value)}
          name="name"
          id="name"
        />
      </div>

      <div className="buttons">
        <button type="submit" >
          Get country
        </button>
        <button type="button" onClick = { onCancel }>
          Cancel
        </button>
      </div>
      
      <div className="control">
        <label htmlFor="name">Post:</label>
        <input
          value={postCountryName}
          onChange={(e) => setostCountryName(e.target.value)}
          name="name"
          id="name"
        />
      </div>

      <div className="buttons">
        <button type="submit">
          Post country
        </button>
        <button type="button" onClick = { onCancel }>
          Cancel
        </button>
      </div>

    </form>
  );
};

export default SolarWatch;
