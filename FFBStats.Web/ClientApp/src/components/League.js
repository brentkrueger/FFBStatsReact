import React, { Component } from 'react';

export class League extends Component {

  constructor (props) {
    super(props);
  }

    render() {
        return (
        <div class='league'>
                <h2>{this.props.league.name}</h2>
                <ul>
                    <li><img class='teamLogo' src={this.props.league.loggedInUserStats.logo} /></li>
                    <li>Team Name: {this.props.league.loggedInUserStats.teamName}</li>
                    <li>Teams in league: {this.props.league.numTeams}</li>
                </ul>
                <h3>My Team Stats</h3>
                <ul>
                    <li>High Score: {this.props.league.loggedInUserStats.highScore} in week {this.props.league.loggedInUserStats.highScoreWeek}</li>
                    <li>Low Score: {this.props.league.loggedInUserStats.lowScore} in week {this.props.league.loggedInUserStats.lowScoreWeek}</li>
                </ul>
                <h3>All Teams Stats</h3>
                <ul>
                    <li>High Score: {this.props.league.leagueWideStats.highScore} by {this.props.league.leagueWideStats.highScoreTeamName} in week {this.props.league.leagueWideStats.highScoreWeek}</li>
                    <li>Low Score: {this.props.league.leagueWideStats.lowScore} by {this.props.league.leagueWideStats.lowScoreTeamName} in week {this.props.league.leagueWideStats.lowScoreWeek}</li>
                </ul>
            </div>
    );
  }
}
