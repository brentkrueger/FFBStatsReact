import React, { Component } from 'react';

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
                    <ul>
                    <li>{game.gameKey}</li>
                    <li>{game.gameId}</li>
                        <li>{game.name}</li>
                        </ul>
                ))}
         
         </div>
    );
  }
}
