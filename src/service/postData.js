
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

    TrashNotesService(NoteId)
    {
        var JwtToken = localStorage.getItem('Token')
        console.log('Trashhhhh',JwtToken,NoteId)
        var header = { headers: { Authorization: `Bearer ${JwtToken}` } };
        console.log('Header',header);
        
        return axios.post("https://localhost:44313/api/Notes/"+NoteId+"/Trash",null,header)
    }

    

    ArchiveNotesService(NoteId)
    {
        console.log('This is axios note id', NoteId)
        var JwtToken = localStorage.getItem('Token')
        var header = { headers: { Authorization: `Bearer ${JwtToken}` } };
        console.log('This is token is Archive axios', header)
        return axios.post("https://localhost:44313/api/Notes/"+NoteId+"/Archive",null,header)
    }

    GetAllArchiveNotesService()
    {
        console.log("GetNotesService");
        
        var JwtToken = localStorage.getItem('Token')
        console.log("This is get notes service", JwtToken);
        return axios.get(`https://localhost:44313/api/Notes/GetArchiveNotes`, {headers:{Authorization: `bearer ${JwtToken}`}})
    }

     GetAllTrashNotesService()
    {
        console.log("GetNotesService");
        
        var JwtToken = localStorage.getItem('Token')
        console.log("This is get notes service", JwtToken);
        return axios.get(`https://localhost:44313/api/Notes/GetTrashNotes`, {headers:{Authorization: `bearer ${JwtToken}`}})
    }

    GetColorService(data)
    {
        var JwtToken = localStorage.getItem('Token')
        console.log("Axios  Id  ",data.Id)
        console.log("Axios Color   ",data.color)

        return axios.put("https://localhost:44313/api/Notes/"+data.Id+"/"+data.color+"/color",null,{headers:{Authorization: `bearer ${JwtToken}`}})

    }
}
