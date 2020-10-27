import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { NavMenuAuthenticated } from './NavMenuAuthenticated';

export class Layout extends Component {
    static displayName = Layout.name;

    constructor(props) {
        super(props);

        this.state = {
        };
    }

    componentDidMount() {
        fetch('/api/account/IsAuthenticated')
            .then(response => response.json())
            .then(data => {
                if (data === true) {
                    this.setState({
                        isAuthenticated: true
                    });
                } else {
                    this.setState({
                        isAuthenticated: false
                    });
                }
            });
    }

  render () {
    return (
      <div>
            {this.state.isAuthenticated ? <NavMenuAuthenticated /> : <NavMenu /> }
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
