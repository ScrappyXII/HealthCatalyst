# Health Catalyst - People Search Application

## Business Requirements

* The application accepts search input in a text box and then displays in a pleasing style a list of people where any part of their first or last name matches what was typed in the search box (displaying at least name, address, age, interests, and a picture).
* Solution should either seed data or provide a way to enter new users or both
* Simulate search being slow and have the UI gracefully handle the delay

## Technical Requirements

* A Web Application using WebAPI and a front-end JavaScript framework (e.g., Angular, AngularJS, React, Aurelia, etc.)
* Use an ORM framework to talk to the database
* Unit Tests for appropriate parts of the application

## Solution

### Overview

The solution was built and tested using Visual Studio 2019 Community Edition for Mac (compatible and also validated with Visual Studio 2019 Community Edition for Windows):
* It is built on .NET Core 2.1
* It has models for Person w/ a People context, a controller for People, and a View for the Search GUI
* It leverages an Sqlite DB (including a sample seeded DB) and uses object relational mapping
* It uses React (w/ Node) 
* It contains unit tests for the People controller and the Person model

### Pre-reqs

* Visual Studio 2019 (Mac or Windows) Community Edition
* .NET Core 2.1
* Node (see www.nodejs.org) 

NOTE: this is not compatible with Visual Studio 2017 or earlier editions.

### Running the Solution

* Clone the project from Github
* Open the HealthCatalyst solution in Visual Studio
* Select the HealthCatalyst project in Visual Studio
* Build All (or Rebuild All) in Visual Studio
* Run the project in Visual Studio

The Health Catalyst People Search app will be loaded in a browser. You can then:
* Click Search People and it will display all the people in the seeded DB
* Enter search criteria to filter the results, such as entering the letter M and clicking Search People
* Enter search criteria that will display no results, such as entering a ! and clicking Search People

### Simulating Slow Searches

To simulate a slow search, please use Google Chrome. 
* With the HealthCatalyst project running, open Chrome to the Health Catalyst People Search site (for example for my environment this was localhost:5000 on Mac and localhost:50645 on Windows; use the URL relevant to your development envionment) 
* Open the Developer Tools in Chrome (for example, via View -> Developer -> Developer Tools)
* Go to the Network tab in the Chrome Developer Tools, and change the Throttling option from Online to a slower option, try a Search and see how the UI handles delay
* Go to the Network tab in the Chrome Developer Tools, and change the Throttling option from Online to the Offline option, try a Search and see how UI handles no connection
