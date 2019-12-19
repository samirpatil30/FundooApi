import axios from 'axios';

export default class ForgotPasswordAxiosService  {
    
    ForgotPasswordService(userData){
        console.log(" data in axios service",userData);
        
        return axios.post(`https://localhost:44313/api/Account/ForgotPassword`,  userData)

    }
     
            
    }
