import React, { Component } from 'react';
import {  Card, CardBody, CardGroup, Col, Container, Form, Input, InputGroup, InputGroupAddon, InputGroupText, Row } from 'reactstrap';   
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import { Button } from '@material-ui/core';
import { ThemeProvider ,createMuiTheme} from '@material-ui/core'
import  AxiosService  from '../service/postData';


const theme = createMuiTheme({
   
   
});

var resetPasswordOfUser= new AxiosService;
export class Reset extends Component{
    constructor(props)
    {
      super(props);
  
      this.state= {
        password:''
      }
  
      this.resetPassword= this.resetPassword.bind(this);
      this.onchange = this.onchange.bind(this);
    }
  
    resetPassword()
    {
      // console.log(this.state);
        var data = {
            token: this.props.match.params.token,
          password: this.state.password}
                 
          resetPasswordOfUser.ResetPasswordService(data).then(response=>{
                      console.log(" response in ",response);
                      
                    })
              
    }
  
    onchange(e)
    {
      this.setState({[e.target.name]: e.target.value});
      console.log(this.state);
    }
  render(){

      return(

          <div className="reset" id="div-id">
             
              <h2>Fundoo</h2>            
              <h5>Account Recovery</h5>
              <label> Enter Your new password</label>
              
            <div>
                <ThemeProvider theme={theme}>
 
                    <TextField 
                        id="text-log-in"
                        label="Password"
                        name="password"  
                        onChange={this.onchange}         
                        margin="normal"
                        variant="outlined"
                    /> 
                    
                </ThemeProvider>

            </div>

            <div className="div-log-forget">
                    <Button id="button-sigin" variant="outlined" onClick={this.resetPassword} >
                            Reset Password </Button>

            </div>
</div>

      )
  }
}
