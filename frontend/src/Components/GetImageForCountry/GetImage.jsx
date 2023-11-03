import { useState } from "react";
import { useLocation } from 'react-router-dom';
import Loading from "../Loading/Loading";
import { Image } from "../Styles/Image.styled";
import { TextContainer, TextWrapper } from "../Styles/TextContainer.styled";

const GetImage = ({ city }) => {
  const location = useLocation();
  const state = location.state;
  const [loading, setLoading] = useState(false);

  if (loading) {
    return <Loading />;
  }

  console.log(state);

  return (
    <div>
      <Image src={city} alt="City" />
      {state && (
        <TextWrapper>
          <TextContainer>Name:</TextContainer>
          <TextContainer>{state.data.name}</TextContainer>
          <TextContainer>Sunrise time:</TextContainer>
          <TextContainer>{state.data.sunRiseTime}</TextContainer>
          <TextContainer>Sunset time:</TextContainer>
          <TextContainer>{state.data.sunSetTime}</TextContainer>
        </TextWrapper>
      )}
    </div>
  );
};

export default GetImage;