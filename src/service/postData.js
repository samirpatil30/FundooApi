
import axios from 'axios';

export default class AxiosService  {
    
    loginService(userData)
    {
        console.log(" data in axios service",userData);    
        return axios.post(`https://localhost:44313/api/Account/login`,  userData)
    }
     

    ForgotPasswordService(userData)
    {
        console.log(" forgot password in axios service",userData);
        return axios.post(`https://localhost:44313/api/Account/ForgotPassword`,  userData)
    }
        
    ResetPasswordService(userData)
    {
        console.log("Forfot password service");
        return axios.post(`https://localhost:44313/api/Account/Reset`,  userData)
    }

    AddNotesService(userData)
    {
        var JwtToken = localStorage.getItem('Token')      
        let response = axios.post(`https://localhost:44313/api/Notes`, userData, {headers:{Authorization: `bearer ${JwtToken}`}} );
        console.log(response);
        
        return response;
    }

     GetNotesService()
    {
        console.log("GetNotesService");
        
        var JwtToken = localStorage.getItem('Token')
       console.log("This is get notes service", JwtToken);
        return axios.get(`https://localhost:44313/api/Notes/Notes`, {headers:{Authorization: `bearer ${JwtToken}`}})
    }
}
