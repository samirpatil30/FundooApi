
import axios from 'axios';

export default class AxiosService  {
    
     url= 'https://localhost:44313';
    loginService(userData)
    {
        console.log(" data in axios service",userData);    
        return axios.post(this.url+'/api/Account/login',  userData)
    }
     

    ForgotPasswordService(userData)
    {
        console.log(" forgot password in axios service",userData);
        return axios.post(this.url+'/api/Account/ForgotPassword',  userData)
    }
        
    ResetPasswordService(userData)
    {
        console.log("Forfot password service");
        return axios.post(`https://localhost:44313/api/Account/Reset`,  userData)
    }

    AddNotesService(userData)
    {
        var JwtToken = localStorage.getItem('Token');
        var header={headers:{Authorization: `bearer ${JwtToken}`}};      
        let response = axios.post(`https://localhost:44313/api/Notes`, userData, header);
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

    TrashNotesService(notesId)
    {
        var JwtToken = localStorage.getItem('Token')
        console.log('Trashhhhh',JwtToken,notesId)
        var header = { headers: { Authorization: `Bearer ${JwtToken}` } };
        console.log('Header',header);
        
        return axios.delete('https://localhost:44313/api/Notes/Trash',notesId,header)
    }
}
