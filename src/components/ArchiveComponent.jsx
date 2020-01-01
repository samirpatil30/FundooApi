import React, { Component } from 'react';
import ArchiveIcon from '@material-ui/icons/Archive';
import Tooltip from '@material-ui/core/Tooltip';
import  AxiosService  from '../service/postData';
import IconButton from '@material-ui/core/IconButton';
import UnarchiveOutlinedIcon from '@material-ui/icons/UnarchiveOutlined';


var axiosObject = new AxiosService;
export default class ArchiveComponent extends Component
{

ArchiveNotes=()=> {
           // console.log('this is delete note function', this.props )
    var id=   this.props.noteid.id
     console.log('Archive note id in Archive()',id)
    
   
              axiosObject.ArchiveNotesService(id).then(response=>{
                 console.log(" response in ",response);
                  })

            .catch(error => {
            console.log('def',error.response)
            });
    }


  render()
  {
    return(
      <div className="Archive-top-div">

       {this.props.noteid.Archive === true ?   <Tooltip title="Unarchive">
                              <IconButton  size="small" color="black">
                              <Badge  color="secondary">
                              < ArchiveIcon fontSize="inherit"/>
                              </Badge>
                              </IconButton>
                              </Tooltip>
                          
                              : <Tooltip title="Archive">
                              <IconButton size="small" onClick={this.ArchiveNotes} color="black">
                              <UnarchiveOutlinedIcon fontSize="inherit" />   
                              </IconButton>
                              </Tooltip>  }
      </div>
    )
  }

}
