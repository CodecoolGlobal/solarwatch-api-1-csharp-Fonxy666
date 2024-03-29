import { useState } from "react";
import { ButtonContainer } from "../../Components/Styles/ButtonContainer.styled";
import { ButtonRowContainer } from "../../Components/Styles/ButtonRow.styled";
import { TextContainer } from "../Styles/TextContainer.styled";
import { Form, FormRow } from "../Styles/Form.styled";
import { InputForm, InputWrapper } from "../Styles/Input.styled";

const UserForm = ({ onSave, user, onCancel }) => {
    const [email, setEmail] = useState(user?.email ?? "");
    const [username, setUsername] = useState(user?.username ?? "");
    const [password, setPassword] = useState(user?.password ?? "");

    const onSubmit = (e) => {
        e.preventDefault();

        if (user) {
            return onSave({
                ...user,
                email,
                username,
                password
            });
        }

        return onSave({
            email,
            username,
            password
        });
    };

    return (
        <Form onSubmit={onSubmit}>
            <FormRow className="control">
                <TextContainer>E-mail:</TextContainer>
                <InputWrapper>
                    <InputForm
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        name="email"
                        id="email"
                        placeholder="E-mail"/>
                </InputWrapper>
            </FormRow>
            <FormRow className="control">
                <TextContainer>Username:</TextContainer>
                <InputWrapper>
                    <InputForm
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        name="username"
                        id="username"
                        placeholder="Username"
                        autoComplete="off"/>
                </InputWrapper>
            </FormRow>
            <FormRow className="control">
                <TextContainer>Password:</TextContainer>
                <InputWrapper>
                    <InputForm
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        name="password"
                        id="password1"
                        placeholder="Password"
                        autoComplete="off"
                        type="password"/>
                </InputWrapper>
            </FormRow>
            <FormRow className="control">
                <TextContainer>Repeat Password:</TextContainer>
                <InputWrapper>
                    <InputForm
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        name="password"
                        id="password2"
                        placeholder="Password"
                        autoComplete="off"
                        type="password"/>
                </InputWrapper>
            </FormRow>
            <ButtonRowContainer>
                <ButtonContainer type="submit">
                    {user ? "Update User" : "Create User"}
                </ButtonContainer>
                <ButtonContainer type="button" onClick = { onCancel }>
                    Cancel
                </ButtonContainer>
            </ButtonRowContainer>
        </Form>
    );
};

export default UserForm;
