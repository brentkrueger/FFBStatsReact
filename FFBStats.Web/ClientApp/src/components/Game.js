import React, { Component } from 'react';

export class Game extends Component {

  constructor (props) {
    super(props);
  }

    render() {
        return (
        <div class='game'>
        <ul>
            <li>{this.props.game.season}</li>
            <li>{this.props.game.name}</li>
                </ul>
            </div>
    );
  }
}
