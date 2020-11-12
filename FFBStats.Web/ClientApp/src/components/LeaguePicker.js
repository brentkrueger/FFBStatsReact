import React, { Component } from 'react';
import { League } from './League';
import { Spinner } from './Spinner';
import { Game } from './Game';

export class LeaguePicker extends Component {

    constructor(props) {
        super(props);
        this.state = {
            leagues: []
        };
    }

    componentDidMount() {

        var gameKey = this.props.match.params.gameKey;

        const requestOptions = {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        };

        this.setState({ fetchInProgress: true });

        fetch('/api/interactive/GetLeagues/' + gameKey, requestOptions)
            .then(response => response.json())
            .then(data => {
                this.setState({
                    leagues: data,
                    fetchInProgress: false
                });
            });
    }

    render() {
        const { leagues } = this.state;
    return (
      <div>
            <div class="header">Select a league:</div>
            {
                this.state.fetchInProgress ?
                    <Spinner /> :
                    leagues.map(league => (
                        <League league={league}></League>
                        ))
            }
         </div>
    );
  }
}
