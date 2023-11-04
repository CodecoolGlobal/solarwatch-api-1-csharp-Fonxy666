import { useState } from "react";
import { ButtonContainer } from "../Styles/ButtonContainer.styled";
import { ButtonRowButtonContainer } from "../../Components/Styles/ButtonRow.styled";
import { TextContainer } from "../Styles/TextContainer.styled";
import { FormRow, Form } from "../Styles/Form.styled";
import { InputForm, InputWrapper, SelectForm } from "../Styles/Input.styled";

const SolarWatch = ({ onGet, onPost, onDelete, country, onCancel }) => {

  const [getCountryName, setGetCountryName] = useState(country?.name ?? "");
  const [deleteCountryName, setDeleteCountryName] = useState(country?.name ?? "");
  const [postCountryName, setPostCountryName] = useState(country?.name ?? "");

  const [action, setAction] = useState("");

  const handleGetSubmit = (e) => {
    e.preventDefault();
    onGet(getCountryName);
  };

  const handlePostSubmit = (e) => {
    e.preventDefault();
    onPost(postCountryName);
  };

  const handleDeleteSubmit = (e) => {
    e.preventDefault();
    onDelete(deleteCountryName);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (action === "get") {
      handleGetSubmit(e);
    } else if (action === "post") {
      handlePostSubmit(e);
    } else if (action === "delete") {
      handleDeleteSubmit(e);
    }
  };


  return (
    <Form onSubmit={handleSubmit}>
      
      <FormRow>
        <TextContainer>Get:</TextContainer>
        <InputWrapper>
          <InputForm
            value={getCountryName}
            onChange={(e) => setGetCountryName(e.target.value)}
            name="name"
            id="name"
            placeholder="City"
            autocomplete="off"
          />
        </InputWrapper>

        <ButtonRowButtonContainer>
          <ButtonContainer type="submit" onClick={() => setAction("get")}>
            Get country
          </ButtonContainer>
          <ButtonContainer type="button" onClick = { onCancel }>
            Cancel
          </ButtonContainer>
        </ButtonRowButtonContainer>
      </FormRow>

      
      <FormRow>
        <TextContainer>Post:</TextContainer>
        <InputWrapper>
          <InputForm
            value={postCountryName}
            onChange={(e) => setPostCountryName(e.target.value)}
            name="name"
            id="name"
            placeholder="City"
            autocomplete="off"
          />
        </InputWrapper>

        <ButtonRowButtonContainer>
          <ButtonContainer type="submit" onClick={() => setAction("post")}>
            Post country
          </ButtonContainer>
          <ButtonContainer type="button" onClick = { onCancel }>
            Cancel
          </ButtonContainer>
        </ButtonRowButtonContainer>
      </FormRow>

      <FormRow>
        <TextContainer>Delete:</TextContainer>
        <InputWrapper>
          <InputForm
            value={deleteCountryName}
            onChange={(e) => setDeleteCountryName(e.target.value)}
            name="name"
            id="name"
            placeholder="Id"
            autocomplete="off"
          />
        </InputWrapper>

        <ButtonRowButtonContainer>
          <ButtonContainer type="submit" onClick={() => setAction("delete")}>
            Delete country
          </ButtonContainer>
          <ButtonContainer type="button" onClick = { onCancel }>
            Cancel
          </ButtonContainer>
        </ButtonRowButtonContainer>
      </FormRow>

    </Form>
  );
};

export default SolarWatch;
