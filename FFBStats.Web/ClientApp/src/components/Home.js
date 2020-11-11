import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Welcome to FFB Stats</h1>
        <p>Please login to start viewing stats for your league</p>
       </div>
    );
  }
}
