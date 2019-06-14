import React, { Component } from 'react';
import { Route } from 'react-router';
import { PeopleSearch } from './PeopleSearch';

export default class View extends Component {
  displayName = View.name

  render() {
    return (
            <Route path='/' component={PeopleSearch} />
    );
  }
}
