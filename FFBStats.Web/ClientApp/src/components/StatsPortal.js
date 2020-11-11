/// <reference path="game.js" />

import React, { Component } from 'react';
import { Game } from './Game';
import { Spinner } from './Spinner';

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

        this.setState({ fetchInProgress: true });

        fetch('/api/interactive/GetGames', requestOptions)
            .then(response => response.json())
            .then(data => {
                this.setState({
                    games: data,
                    fetchInProgress: false
                });
            });
    }

    render() {
        const { games } = this.state;
    return (
      <div>
            <h1>Stats Portal</h1>
            {
                this.state.fetchInProgress ?
                    <Spinner /> :
                    
                        games.map(game => (

                            <Game game={game}></Game>


                        ))
                    
            }
         </div>
    );
  }
}
