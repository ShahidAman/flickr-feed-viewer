import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Navbar, Nav, Form, FormControl, Button } from "react-bootstrap";
import ImageContainer from "./components/ImageContainer";
class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      searchText: "",
      feedName: "",
      images: [],
      images2D: []
    };
    this.handleTextInput = this.handleTextInput.bind(this);
    this.handleSearchSubmit = this.handleSearchSubmit.bind(this);
    this.bootStrapFromFeed = this.bootStrapFromFeed.bind(this);
    this.callFlickrApi = this.fetchImagesAndSetState.bind(this);
  }

  componentDidMount() {
    this.bootStrapFromFeed();
  }
  handleTextInput(event) {
    this.setState({ searchText: event.target.value });
  }

  handleSearchSubmit(event) {
    event.preventDefault();
    this.fetchImagesAndSetState("http://localhost:5000/feeds/search?tags=" + this.state.searchText);
  }

  bootStrapFromFeed() {
    this.fetchImagesAndSetState("http://localhost:5000/feeds")
  }

fetchImagesAndSetState(url){
  fetch(url)
      .then((res) => res.json())
      .then((data) => {
        let array2D = [];
        var i, j, temparray, chunk = 3;
        for (i = 0, j = data.items.length; i < j; i += chunk) {
          temparray = data.items.slice(i, i + chunk);
          array2D.push(temparray);
        }
        this.setState({
          feedName: data.title,
          images: data.items,
          images2D:array2D
        });
      });
}


  render() {
    return (
      <>
        <Navbar bg="dark" variant="dark" expand="lg">
          <Navbar.Brand href="/">Flickr Image Viewer</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="mr-auto">
              <Nav.Link href="/">Home</Nav.Link>
              <Nav.Link href="#about">About</Nav.Link>
            </Nav>
            <Form inline onSubmit={(e) => this.handleSearchSubmit(e)}>
              <FormControl
                type="text"
                value={this.state.searchText}
                placeholder="Search for pics, e.g cat"
                className="mr-sm-2"
                size="lg"
                onChange={(e) => this.handleTextInput(e)}
              />
              <Button
                variant="outline-success"
                onClick={(e) => this.handleSearchSubmit(e)}
              >
                Search
              </Button>
            </Form>
          </Navbar.Collapse>
        </Navbar>
        <ImageContainer
          images={this.state.images}
          feedName={this.state.feedName}
          images2D = {this.state.images2D}
        />
      </>
    );
  }
}

export default App;

