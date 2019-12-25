import React, { Component } from 'react';
import AccessAlarmsIcon from '@material-ui/icons/AccessAlarms';
import PersonAddIcon from '@material-ui/icons/PersonAdd';
import PaletteIcon from '@material-ui/icons/Palette';
import ImageIcon from '@material-ui/icons/Image';
import ArchiveIcon from '@material-ui/icons/Archive';
import UnarchiveIcon from '@material-ui/icons/Unarchive';
import MoreVertIcon from '@material-ui/icons/MoreVert';
import Menu from '@material-ui/core/Menu';
import IconButton from '@material-ui/core/IconButton';
import Badge from '@material-ui/core/Badge';
import Tooltip from '@material-ui/core/Tooltip';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import MenuList from '@material-ui/core/MenuList';
import MenuItem from '@material-ui/core/MenuItem';
import Paper from '@material-ui/core/Paper';
import '../css/IconsCSS.css';
import  AxiosService  from '../service/postData';
import Collaborator from './Collaborator'
import UnarchiveOutlinedIcon from '@material-ui/icons/UnarchiveOutlined';
var axiosObject = new AxiosService;
export default class Icons extends Component
{
    constructor(props){
        super(props);
        this.state={
            show:'false',
            anchorEl: null,
            showCollabrator: false
        }
        
    //console.log('this is delete note prop', this.props)

        this.handleClick=this.handleClick.bind(this)

    }
   
      handleClick = event => {
        this.setState({ anchorEl: event.currentTarget });
        // console.log('scsc',event.currentTarget)
      };
    
      handleClose = () => {
        this.setState({ anchorEl: null });
      
      };

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


  DeleteNote=()=>{
  
   // console.log('this is delete note function', this.props )
    var id=  this.props.noteid.id
     console.log('delete note id in Delete()',id)
    
   
           axiosObject.TrashNotesService(id).then(response=>{
         console.log(" response in ",response);
   })

   .catch(error => {
    console.log('def',error.response)
});
  }

  onchange(e)
  {
    this.setState({[e.target.name]: e.target.value});
    console.log(this.state);
  }

  openCollabrator=() =>
  {
    this.setState({
      showCollabrator: !this.state.showCollabrator
    })
  }
    render(){
      console.log('Iconss',this.props.noteid);
      
        const { anchorEl } = this.state;
        return(
          
            <div className="icon-div-flex">
                           { this.state.showCollabrator ?
                                <Collaborator /> : null}

                              {this.props.noteid.Archive === false ?   <Tooltip title="Unarchive">
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

                                                       
                              
                              <Tooltip title="Reminder">
                              <IconButton  size="small" color="black">
                              <Badge  color="secondary">
                              < AccessAlarmsIcon fontSize="inherit"/>
                              </Badge>
                              </IconButton>
                              </Tooltip>
                          
                              
                              
                            
                              <Tooltip title="Collaborate" >
                              <IconButton  size="small" color="black" onClick={this.openCollabrator}>
                               
                              <Badge  color="secondary">
                              <PersonAddIcon fontSize="inherit" />
                              </Badge>
                              </IconButton>
                              </Tooltip>
                              

                             
                              <Tooltip title="Color" >
                              <IconButton  size="small" color="black">
                              <Badge  color="secondary">
                              <PaletteIcon fontSize="inherit" />
                              </Badge>
                              </IconButton>
                              </Tooltip>
                                
                

                              
                              <Tooltip title="Image">
                              <IconButton  size="small" color="black">
                              <Badge  color="secondary">
                              <ImageIcon fontSize="inherit" />
                              </Badge>
                              </IconButton>
                              </Tooltip>
                                  

     
                            
                              <Tooltip title="More" >
                              <IconButton  size="small" color="balck" onClick={this.handleClick}>
                              <Badge  color="secondary">
                              <MoreVertIcon fontSize="inherit" 
                                  aria-owns={anchorEl ? 'simple-menu' : 'simple-menu'}                            
                                  aria-haspopup="true"
                                  onClick={this.handleClick}
                                 />
                              </Badge>
                              </IconButton>
                              </Tooltip> 
                                  
                              <div className="Menu">  
                                 <Menu
                                        id="menu-size"
                                        anchorEl={this.state.anchorEl}
                                        open={Boolean(anchorEl)}
                                        onClose={this.handleClose}>

                                    <MenuItem onClick={this.handleClose} onClick={this.DeleteNote} >Delete Note </MenuItem>
                                    <MenuItem onClick={this.handleClose}>Add Label</MenuItem>
                                  </Menu>
                            
                                </div>
    
            </div>
        )
    }
} 