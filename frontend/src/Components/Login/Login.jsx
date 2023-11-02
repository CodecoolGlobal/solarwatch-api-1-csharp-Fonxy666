import { useState } from "react";
import Loading from "../Loading/Loading";
import { ButtonContainer } from "../../Components/Styles/Buttoncontainer.styled";
import { ButtonRowContainer } from "../../Components/Styles/ButtonRow.styled";
import { TextContainer } from "../Styles/TextContainer.styled";
import { LoginForm } from "../Styles/LoginForm.styled";
import { InputForm, InputWrapper } from "../Styles/Input.styled";

const Login = ({ onLogin, user, onCancel }) => {

  const [loading, setLoading] = useState(false);
  const [username, setUsername] = useState(user?.username ?? "");
  const [password, setPassword] = useState(user?.password ?? "");

  const onSubmit = (e) => {
    e.preventDefault();

    return onLogin({
        username,
        password
    });
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <LoginForm className="EmployeeForm" onSubmit={onSubmit}>

      <TextContainer>Username:</TextContainer>
      <InputWrapper>
        <InputForm
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          name="username"
          id="username"
        />
      </InputWrapper>

      <TextContainer>Password:</TextContainer>
      <InputWrapper>
        <InputForm
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          name="password"
          id="password"
        />
      </InputWrapper>

      <ButtonRowContainer className="buttons">
        <ButtonContainer className = "button" type="submit">
          Login
        </ButtonContainer>
        <ButtonContainer className = "button" type="button" onClick = { onCancel }>
          Cancel
        </ButtonContainer>
      </ButtonRowContainer>
    </LoginForm>
  );
};

export default Login;
