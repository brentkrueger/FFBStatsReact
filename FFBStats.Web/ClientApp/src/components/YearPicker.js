import React, { Component } from 'react';
import { Game } from './Game';
import { Spinner } from './Spinner';
import { NavLink } from 'react-router-dom';

export class YearPicker extends Component {

    constructor(props) {
        super(props);
        this.state = {
            games: []
        };
    }


    componentDidMount() {

        const requestOptions = {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        };

        this.setState({ fetchInProgress: true });

        fetch('/api/LeagueStats/GetGames', requestOptions)
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
            <div class="header">Select a year:</div>
            {
                this.state.fetchInProgress ?
                    <Spinner /> :
                    games.map(game => (
                        <NavLink to={`/leaguepicker/${game.gameKey}`}>
                            <Game game={game}></Game>
                        </NavLink>
                        ))
            }
         </div>
    );
  }
}
