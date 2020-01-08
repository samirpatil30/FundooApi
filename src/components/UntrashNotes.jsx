import React from 'react'
import IconButton from '@material-ui/core/IconButton';
import Tooltip from '@material-ui/core/Tooltip';
import RestoreFromTrashOutlinedIcon from '@material-ui/icons/RestoreFromTrashOutlined';
import  AxiosService  from '../service/postData';
import DeleteForeverOutlinedIcon from '@material-ui/icons/DeleteForeverOutlined';
import "../css/UntrashIcons.css";

var axiosObject = new AxiosService;
export default class UntrashNotes extends  React.Component
{
  constructor(props)
  {
    super(props)

  }

  DeleteNote=()=>{
  
   // console.log('this is delete note function', this.props )
    var id=  this.props.noteid.id
     console.log('UnTrashh note id in Delete()',id)
    
   
           axiosObject.TrashNotesService(id).then(response=>{
         console.log(" response in ",response);
          this.props.getTrashNotes()
   })

   .catch(error => {
    console.log('def',error.response)
  });
}
  render()
  {
    return(
           <div className="UntrashIcons">
                <Tooltip title="Untash">
                 <IconButton  size="small" color="balck" onClick={this.DeleteNote}>
                  <RestoreFromTrashOutlinedIcon fontSize="inherit"
                    style={{ width: "20px" }}

                  />
                   </ IconButton>
                </Tooltip>

               <Tooltip title="DeleteForever">
                 <IconButton  size="small" color="balck" onClick={this.DeleteNote}>
                  <DeleteForeverOutlinedIcon fontSize="inherit"
                    style={{ width: "20px" }}

                  />
                   </ IconButton>
                </Tooltip>

                </div>                   

    )
  }
}