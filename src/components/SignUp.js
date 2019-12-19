import React, { Component } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import { Button } from '@material-ui/core';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import { createMuiTheme, responsiveFontSizes } from '@material-ui/core/styles';
import  AxiosService  from '../service/SignUpData';


var signUpService= new AxiosService;
const useStyles = makeStyles(theme => ({
  root: {
    '& > *': {
      margin: theme.spacing(1),
      width: 100,
      
    },
  },
}));


export class SignUp extends Component{
  constructor(props)
  {
    super(props);

    this.state= {
      firstName:'',
      lastName:'',
      userName:'',
      password:'',
      serviceType:''
    }

    this.signUp= this.signUp.bind(this);
    this.onchange = this.onchange.bind(this);
  }

  signUp()
  {
    // console.log(this.state);
      var data = {
                  FirstName: this.state.firstName, 
                  LastName: this.state.lastName,
                  UserName: this.state.userName,                             
                  password : this.state.password,
                  ServiceType: this.state.serviceType}

                  signUpService.SignUpServicesSAMIR(data).then(response=>{
                    console.log(" response in ",response);
                    
                  })
  }

  onchange(e)
  {
    this.setState({[e.target.name]: e.target.value});
    console.log(this.state);
  }
  render()
    {
        const classes = useStyles;
      
        return(
          
         <div className="wrapper">
                 
                  <div className="leftDiv">
                  <h5 className="H6"> Google</h5>
                <div className="firstname">
                <TextField id="FirstNameId"  label="FirstName" name="firstName"  onChange={this.onchange}   variant="outlined" />
                    
                  <TextField id="outlined-basic" label="LastName"  name="lastName" onChange={this.onchange}  variant="outlined" />
                  </div>
                  
                  <div className="username">
                  <TextField id="outlined-basic1" label="Username"  name="userName" onChange={this.onchange}  variant="outlined" />
                 
                  <h6 id= "h61"> you can use letters,numbers and periods</h6>
                  </div>

                  <div className="Password">
                  <TextField id="outlined-basic" label="Password" name="password" onChange={this.onchange}   variant="outlined" />
                  <h6>you can use combination of letters,numbers and periods</h6>
                  </div>

                  <div className="ServiceType">
                  <TextField id="outlined-basic" label="serviceType" name="serviceType" onChange={this.onchange}  variant="outlined" />

                  </div>
         
                  <Button id="SignUpButton"  variant="contained" onClick={this.signUp} >SignUp</Button>
              
          
              </div>
                  
                 
           </div>
            
  
 
  );
}
}