import React, { Component } from 'react';
import {  Card, CardBody, CardGroup, Col, Container, Form, Input, InputGroup, InputGroupAddon, InputGroupText, Row } from 'reactstrap';   
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import { Button } from '@material-ui/core';
import { ThemeProvider ,createMuiTheme} from '@material-ui/core'
import  AxiosService  from '../service/postData';
import "../css/ForgotPasswordCSS.css";

const theme = createMuiTheme({
   
    overrides:{
        MuiInputLabel:{
            formControl:{
                top:"-9px",
                left:"9%"
            }
        },


        MuiInputBase:{
            root:{
                height:"35px",
                width:"80%",
                left:"10%"
            }
        }
    }
  });
var forgotService= new AxiosService;
export class ForgotPassword extends Component{
    constructor(props)
    {
      super(props);
  
      this.state= {
        email:''
   }
  
      this.forgotPassword= this.forgotPassword.bind(this);
      this.onchange = this.onchange.bind(this);
    }
  
    forgotPassword()
    {
      // console.log(this.state);
        var data = {email: this.state.email};           
         forgotService.ForgotPasswordService(data).then(response=>{
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

          <div className="div-log" id="div-id">
             
              <h2>Fundoo</h2>            
              <h5>Account Recovery</h5>
              <label> Enter Your Email</label>
              
            <div>
                <ThemeProvider theme={theme}>
 
                    <TextField 
                        id="text-log"
                        label="Email"
                        name="email"  
                        onChange={this.onchange}         
                        margin="normal"
                        variant="outlined"
                    /> 
                   
                </ThemeProvider>

            </div>

            <div className="div-log-forget">
                    <Button id="button-sigin" variant="outlined" onClick={this.forgotPassword} >
                            Send Mail </Button>

            </div>
</div>

      )
  }
}
