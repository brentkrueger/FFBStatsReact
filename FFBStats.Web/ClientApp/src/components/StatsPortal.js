import React, { Component } from 'react';

export class StatsPortal extends Component {
 
    componentDidMount() {

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title: 'React POST Request Example' })
        };

        fetch('/api/interactive/GetLeagues', requestOptions)
            .then(response => response.json())
            .then(data => {
               debugger
            });
    }

  render () {
    return (
      <div>
        <h1>Stats Portal</h1>
         </div>
    );
  }
}
