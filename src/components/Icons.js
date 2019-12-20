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

var axiosObject = new AxiosService;
export default class Icons extends Component
{
    constructor(props)
    {
        super(props);
        this.state={
            show:'false',
            anchorEl: null
        }
        
    console.log('this is delete note', this.props)

        this.handleClick=this.handleClick.bind(this)
    }
   
      handleClick = event => {
        this.setState({ anchorEl: event.currentTarget });
        // console.log('scsc',event.currentTarget)
      };
    
      handleClose = () => {
        this.setState({ anchorEl: null });
      
      };

    
DeleteNote()
  {
  
    console.log('this is delete note function', this.props.noteid.id)
    var id=  {noteId : this.props.noteid.id}
    console.log('delete note id',id)
    
    // console.log(this.state);
          axiosObject.TrashNotesService(id).then(response=>{
        console.log(" response in ",response);
  })
  }

  onchange(e)
  {
    this.setState({[e.target.name]: e.target.value});
    console.log(this.state);
  }

    render(){
        const { anchorEl } = this.state;
        return(
          
            <div className="icon-div-flex">
                            
                               <Tooltip title="Archive">
                                <IconButton color="black">
                               <ArchiveIcon  className="bottom-icon-list"/>   
                                </IconButton>
                               </Tooltip>

                            <Tooltip title="Reminder">
                              <IconButton color="black">
                              <Badge  color="secondary">
                              < AccessAlarmsIcon  className="bottom-icon-list"/>
                              </Badge>
                              </IconButton>
                              </Tooltip>

                              <Tooltip title="Collaborate" >
                              <IconButton color="black">
                              <Badge  color="secondary">
                              <PersonAddIcon  className="bottom-icon-list"/>
                              </Badge>
                              </IconButton>
                              </Tooltip>

                               <Tooltip title="Color" >
                               <IconButton color="black">
                                <Badge  color="secondary">
                                <PaletteIcon  className="bottom-icon-list"/>
                                </Badge>
                                </IconButton>
                                </Tooltip>
                
                                <Tooltip title="Image">
                                <IconButton color="black">
                                <Badge  color="secondary">
                                <ImageIcon  className="bottom-icon-list"/>
                                </Badge>
                                </IconButton>
                                </Tooltip>

     
                                 <Tooltip title="More" >
                                 <IconButton color="inherit" onClick={this.handleClick}>
                                 <Badge  color="secondary">
                                 <MoreVertIcon  className="bottom-icon-list"
                                  aria-owns={anchorEl ? 'simple-menu' : 'simple-menu'}
                            
                                  aria-haspopup="true"
                                  onClick={this.handleClick}
                                 />
                                 </Badge>
                                 </IconButton>
                                 </Tooltip> 

                                  <div className="Menu">  
                                    <Menu
                                        id="simple-menu"
                                        anchorEl={this.state.anchorEl}
                                        open={Boolean(anchorEl)}
                                        onClose={this.handleClose}>

                                    <MenuItem onClick={this.handleClose} onClick={()=>this.DeleteNote(this.p)} >Delete Note </MenuItem>
                                    <MenuItem onClick={this.handleClose}>Add Label</MenuItem>
         
                                    </Menu>
                                    </div>
                                 <br/><br/><br/>

            </div>
        )
    }
} 
