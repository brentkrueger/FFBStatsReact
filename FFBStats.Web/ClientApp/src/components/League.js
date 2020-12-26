import React, { Component } from 'react';

export class League extends Component {

  constructor (props) {
    super(props);
  }

    render() {
        return (
        <div class='league'>
        <ul>
                    <li>{this.props.league.name}</li>
                    <li><img class='teamLogo' src={this.props.league.logo} /></li>
                    <li>Teams: {this.props.league.numTeams}</li>
                    <li>High Score: {this.props.league.highScore} by {this.props.league.highScoreTeamName} in week {this.props.league.highScoreWeek}</li>
                    <li>Low Score: {this.props.league.lowScore} by {this.props.league.lowScoreTeamName} in week {this.props.league.lowScoreWeek}</li>
                </ul>
            </div>
    );
  }
}
