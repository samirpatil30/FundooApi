import React, { Component } from 'react';
import './App.css';
import {BrowserRouter, Route, Switch, Redirect} from 'react-router-dom' 
import { SignUp } from './components/SignUp';
import { LogIn } from './components/LogIn';
import {ForgotPassword} from './components/ForgotPassword'
import {Reset} from './components/Reset';
import DashBoard from './components/DashBoard'
import Notes from './components/Notes';
 import { Toolbar } from '@material-ui/core';
import GetAllNotes from './components/GetAllNotes' 
import {DotMenu} from './components/DotMenu' 

class App extends Component {

  render() {
    
    return (      
      <BrowserRouter>
      <div className="container">
        <div style={{height: '100%'}}>
          <Toolbar />
      </div>

       
                <Route  path="/login" component={LogIn} />
                <Route path="/signup" component={SignUp}/>
                <Route  path="/reset/:token"  component={Reset} />
                <Route  path= "/ForgotPassword"  component={ForgotPassword} />
                <Route  path= "/Dashboard" component={DashBoard  } />
                <Route   path= "/Notes" component={Notes} />
                <Route  path= "/Dashboard/notes" component={ GetAllNotes} />
                <Route  path= "/Menu" component={ DotMenu} />
         
        
      </div>
      </BrowserRouter>
    );
  }
}

export default App;
