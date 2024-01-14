import { useState } from "react";
import { ButtonContainer } from "../Styles/ButtonContainer.styled";
import { ButtonRowContainer } from "../../Components/Styles/ButtonRow.styled";
import { TextContainer } from "../Styles/TextContainer.styled";
import { FormRow, Form } from "../Styles/Form.styled";
import { InputForm, InputWrapper } from "../Styles/Input.styled";

const PasswordChange = ({ onSave, user, onCancel }) => {
    const [email, setEmail] = useState(user?.email ?? "");
    const [oldPassword, setOldPassword] = useState(user?.oldPassword ?? "");
    const [newPassword, setNewPassword] = useState(user?.newPassword ?? "");

    const onSubmit = (e) => {
        e.preventDefault();

        if (user) {
            return onSave({
                ...user,
                email,
                oldPassword,
                newPassword
            });
        }

        return onSave({
            email,
            oldPassword,
            newPassword
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
                        name="username"
                        id="username"
                        placeholder="Username"
                        autoComplete="off"/>
                </InputWrapper>
            </FormRow>
            <FormRow className="control">
                <TextContainer>Old Password:</TextContainer>
                <InputWrapper>
                    <InputForm
                        value={oldPassword}
                        onChange={(e) => setOldPassword(e.target.value)}
                        name="oldPassword"
                        id="oldPassword"
                        placeholder="Password"
                        autoComplete="off"
                        type="password"/>
                </InputWrapper>
            </FormRow>
            <FormRow className="control">
                <TextContainer>New Password:</TextContainer>
                <InputWrapper>
                    <InputForm
                        value={newPassword}
                        onChange={(e) => setNewPassword(e.target.value)}
                        name="newPassword1"
                        id="newPassword1"
                        placeholder="Password"
                        autoComplete="off"
                        type="password"/>
                </InputWrapper>
            </FormRow>
            <FormRow className="control">
                <TextContainer>Repeat New Password:</TextContainer>
                <InputWrapper>
                    <InputForm
                        value={newPassword}
                        onChange={(e) => setNewPassword(e.target.value)}
                        name="newPassword2"
                        id="newPassword2"
                        placeholder="Password"
                        autoComplete="off"
                        type="password"/>
                </InputWrapper>
            </FormRow>
            <ButtonRowContainer>
                <ButtonContainer type="submit">
                    Create User
                </ButtonContainer>
                <ButtonContainer type="button" onClick = { onCancel }>
                    Cancel
                </ButtonContainer>
            </ButtonRowContainer>
        </Form>
    );
};

export default PasswordChange;
