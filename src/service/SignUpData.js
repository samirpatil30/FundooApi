
import axios from 'axios';

export default class AxiosServiceSignUp  {
    
    SignUpServicesSAMIR(userData){
        console.log(" data in axios servuixe",userData);
        
        return axios.post(`https://localhost:44313/api/Account/Add`,  userData)

    }
}
            