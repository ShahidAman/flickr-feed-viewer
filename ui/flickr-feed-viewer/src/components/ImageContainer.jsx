import React, { Component } from "react";
import { Container, Row, Col, Image, Alert } from "react-bootstrap";

class ImageContainer extends Component {
  render() {
    // iterate through the array of arrays to form a row of images
    const imageRows = this.props.images2D.map((imageArray, index) => {
      const imageCols = imageArray.map((image, imageIndex) => {
        return (
          <Col key={imageIndex}>
            <Image src={image.media.m} rounded width={320} height={240} />
          </Col>
        );
      });
      return (
        <>
          <Row key={index}>{imageCols}</Row>
          <br></br>
        </>
      );
    });

    return (
      <>
        <Alert variant="success">
    <Alert.Heading>Feed: {this.props.feedName}</Alert.Heading>
        </Alert>
        <Container fluid="lg">{imageRows}</Container>
      </>
    );
  }
}

export default ImageContainer;
