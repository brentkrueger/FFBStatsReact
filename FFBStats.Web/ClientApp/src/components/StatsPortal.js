/// <reference path="game.js" />

import React, { Component } from 'react';
import { Game } from './Game';

export class StatsPortal extends Component {

    constructor(props) {
        super(props);
        this.state = {
            games: []
        };
    }


    componentDidMount() {

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title: 'React POST Request Example' })
        };

        fetch('/api/interactive/GetGames', requestOptions)
            .then(response => response.json())
            .then(data => {
                this.setState({
                    games: data
                });
            });
    }

    render() {
        const { games } = this.state;
    return (
      <div>
            <h1>Stats Portal</h1>
            
            {games.map(game => (

                <Game game={game}></Game>

                    
                ))}
         
         </div>
    );
  }
}
