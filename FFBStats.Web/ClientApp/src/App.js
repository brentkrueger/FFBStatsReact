import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import './custom.css'
import { YearPicker } from './components/YearPicker';
import { LeaguePicker } from './components/LeaguePicker';

export default class App extends Component {
  static displayName = App.name;


  render () {
    return (
      <Layout>
            <Route exact path='/' component={Home} />
            <Route exact path='/YearPicker/' component={YearPicker} />
            <Route exact path='/leaguePicker/:gameKey' component={LeaguePicker} />
      </Layout>
    );
  }
}
