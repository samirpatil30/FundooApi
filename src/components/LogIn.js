import React from 'react';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import Link from '@material-ui/core/Link';

import '../css/LogInCSS.css'
import { ThemeProvider ,createMuiTheme} from '@material-ui/core'

import  AxiosService  from '../service/postData';


var logInService = new AxiosService ;

const theme = createMuiTheme({
   
    overrides:{
        MuiInputLabel:{
            formControl:{
                top:"-9px"
            }
        },


        MuiInputBase:{
            root:{
                height:"35px"
                
            }
        },
        MuiFormControl:{
            marginNormal:{
              marginLeft:"20px"
            }
        }
    }
  });

export  class LogIn extends React.Component{

  constructor(props)
  {
    super(props);

    this.state= {
      email:'',
      password:''
    }

    this.logIn= this.logIn.bind(this);
    this.onchange = this.onchange.bind(this);
    this.ForgotPasswordPage=this.ForgotPasswordPage.bind(this);

  }

  logIn()
  {
    // console.log(this.state);
      var data = {
                 
                  Email: this.state.email,                             
                  Password : this.state.password,
                }

                logInService.loginService(data).then(response=>{
                    console.log(" response in ",response);
                    localStorage.setItem('Token', response.data.token)

                    if (response.status === 200) {
                        this.props.history.push('/Dashboard')
                    }
                    else {
                        this.props.history.push('/')
                    }
                  })

                 
  }

  onchange(e)
  {
    this.setState({[e.target.name]: e.target.value});
    console.log(this.state);
  }

  ForgotPasswordPage(){

    console.log(" ",this.props);
    this.props.history.push('/ForgotPassword')
  }

    render(){
      const { products} = this.state;
        return(

            <div className="div-log" id="div-id">
               
                <h2>Fundoo</h2>            
                <h5>SignIn</h5>
                <label id="label-item"> Use Your Fundoo Account</label>
                
   <div>
<ThemeProvider theme={theme}>
   
    
<TextField 
              id="text-log-in"
              label="Email"
              placeholder="Email"            
              margin="normal"
              variant="outlined"
              name="email"
              onChange={this.onchange}
            /> 
            <br/>
            
  <TextField
            variant="outlined"
            margin="normal"
            required
        
            name="password"
            label="Password"
            type="password"
            id="text-log-in"
            autoComplete="current-password"
            name="password"
            onChange={this.onchange}
          />                     
      </ThemeProvider>

      </div>
 

 <div className="button" id="ButtonId">
    <Button id="buttonSignIn" onClick={this.logIn} >SignIn</Button>
    <Button id="ForgotId"   onClick={this.ForgotPasswordPage}> ForgotPassword</Button>                 
  </div>   
  </div>

        )
    }
}
