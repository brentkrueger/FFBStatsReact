import React, { Component } from 'react';
import spinnerImage from '../assets/loading.svg';

export class Spinner extends Component {

  constructor (props) {
    super(props);
  }

    render() {
        return (
            <div class='spinner'>
                Loading...
                <img src={spinnerImage}  />
            </div>
    );
  }
}
