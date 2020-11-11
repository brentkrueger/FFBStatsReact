import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class Game extends Component {

  constructor (props) {
    super(props);

    this.state = {
    };
  }

    render() {

        return (
        <div>
        <ul>
            <li>{this.props.game.gameKey}</li>
            <li>{this.props.game.gameId}</li>
            <li>{this.props.game.name}</li>
                </ul>
            </div>
    );
  }
}
