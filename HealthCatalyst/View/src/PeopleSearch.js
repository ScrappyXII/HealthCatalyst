import React, { Component } from 'react';
import { Button, FormControl, Form } from 'react-bootstrap';
import './spinner.css';

export class PeopleSearch extends Component {
    displayName = PeopleSearch.name

    constructor(properties) {
        super(properties);
        this.state = { people: [], loading: false, query: '', errorMessage: ''};
    }

    searchPeople = e => {
        // set state to "loading" which turns spinner on
        this.setState({ loading: true });

        // generate query based on search input criteria
        const query = 'People/' + this.state.query;

        // Get list of people matching the search criteria
        // Handle situation where backend is unreachable such as if offline
        fetch(query)
            .then(response => response.json()
            .then(data => {
                this.setState({ people: data, loading: false, errorMessage: '' });
            })).catch(error => { 
                this.setState({ people: [], loading: false, query: '', errorMessage: 'You appear to be offline, please try again when back online' });
            })     
    }

    // set search string to text input value
    handleChange = e => {
        this.setState({ query: e.target.value });
    }

    // force click of Submit button
    submitHandler = e => {
        e.preventDefault();
    }

    // display results of People search, any errors, or the loading animation, as appropriate
    renderSearchResults() {
        if (this.state.loading) {
            // display working animation while we are loading data
            return (
                <section className='loader-section'>
                    <div className='flex-item'>
                        <div className='loader'></div>
                    </div>              
                </section>
            )
        }
        else if (this.state.people.length !== 0) {
            // we got results so now we can display the list of people in a pleasing tabular format
            return (
                <div>
                    <table align="center">
                        <thead>
                            <tr>
                                <th>Photo</th>
                                <th></th>
                                <th>Personal Details</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.people.map(person =>
                                <tr height="115" >
                                    <td><img alt="Profile Picture" width="100" height="100" src={person.pictureURL} /></td>
                                    <td width="20"></td>
                                    <td>Name: {person.fullName}<br/>
                                        Age: {person.age}<br/>
                                        Address: {person.address}<br/>
                                        Interests: {person.interests}
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>                       
                </div>
            )
        }
        else {
            return ( 
                // no results. also display error message, if any.
                <div align="center" className={this.state.errorMessage ? "error text-danger" : ""}> {this.state.errorMessage}</div>
            )
        }
    }

    // display search input including edit box and search button
    renderSearchHeader() {
        return (
            <div className='container'>
                <section>
                    <div align="center"><img src="logotype.svg" width="224px" height="56px"/></div>
                    <br/>
                    <div align="center">
                        <Form onSubmit={this.submitHandler} inline>
                            <FormControl
                                type="text"
                                width="224px"
                                value={this.state.query}
                                placeholder="Enter Search Criteria"
                                onChange={this.handleChange}
                            />                     

                            <br/>
                            <Button bsStyle="primary" className="button is-info" onClick={this.searchPeople} >
                                Search People
                            </Button>
                        </Form>
                    </div>
                    <div></div>
                </section>
            </div>
        );
    }

    render() {
        return (
            <div>
                {this.renderSearchHeader()}
                <hr />
                {this.renderSearchResults()}
            </div>
        );
    }
}
