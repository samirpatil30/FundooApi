import React, { Component } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Settings from '@material-ui/icons/Settings';
import AddAlertTwoToneIcon from '@material-ui/icons/AddAlertTwoTone';
import AccessAlarmsIcon from '@material-ui/icons/AccessAlarms';
import PersonAddIcon from '@material-ui/icons/PersonAdd';
import PaletteIcon from '@material-ui/icons/Palette';
import ImageIcon from '@material-ui/icons/Image';
import ArchiveIcon from '@material-ui/icons/Archive';
import UnarchiveIcon from '@material-ui/icons/Unarchive';
import MoreVertIcon from '@material-ui/icons/MoreVert';
import IconButton from '@material-ui/core/IconButton';
import Badge from '@material-ui/core/Badge';
import { ThemeProvider ,createMuiTheme} from '@material-ui/core'
import TextareaAutosize from '@material-ui/core/TextareaAutosize';
import { borderRadius } from '@material-ui/system';
import PropTypes from 'prop-types';
import Tooltip from '@material-ui/core/Tooltip';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import ClickAwayListener from '@material-ui/core/ClickAwayListener';
import Icons from './Icons'
import '../css/NotesCSS.css';

import AxiosService from '../service/postData';
import ArchiveComponent from "./ArchiveComponent";
var jwtDecode = require('jwt-decode');
var addnotes = new AxiosService;
const theme = createMuiTheme({
    overrides: {

        MuiPaper: {
            rounded: {
                width: "100%",
                borderRadius: "10px"

            }
        },
        MuiSvgIcon: {
            root: {

            }
        },
        MuiButton: {
            root: {
                      height: "10px"
                  }
        },
        TextareaAutosize:
        {
            border: "none"
        },
        MuiCardContent: {

            root: {
                padding: "-1px",
            },
        }
    }
});


export default class Notes extends Component
{
  constructor(props)
  {
    super(props)
    this.state = {
        open: false,
        showMe: false,
        notesTitle:'',
        notesDescription:'' 
      };

      
    this.AddNotes= this.AddNotes.bind(this);
    this.onchange = this.onchange.bind(this);
  }

    
      operation = () => {
        this.setState({
          showMe: true,
        });
      };
    
       operationHide = () => {
       
           this.AddNotes();
      };

      AddNotes()
      {
          this.setState({
              showMe: false,

          });
         
          var data = {
                   NotesTitle: this.state.notesTitle, 
                  NotesDescription: this.state.notesDescription 
                }
          console.log(data);
          
          addnotes.AddNotesService(data)
          .then(response => {
            console.log(" response in ", response);
          })
          .catch(err=>{
            console.log(err);
            
          })

      }   

    onchange(e)
    {
      this.setState({[e.target.name]: e.target.value});
      // console.log(this.state);
    }
      
    render(){
        const { open } = this.state;
        return(
            <div className="MainNotesDiv"> 
                
            <ThemeProvider theme={theme}>
           
            <div className="card-div" >
          
               <Card className="card-class">
                  <CardContent id="card-content">   
                      <div className="TextFieldTitle">
                    
                          <TextareaAutosize className="title-text-area" name="notesTitle" onChange={this.onchange} onClick={this.operation} placeholder="Title" />
                          {
                              this.state.showMe ?
                              < div className="TextField2">
                                  <TextareaAutosize className="take-note" name="notesDescription" onChange={this.onchange} aria-multiline="true" aria-label="empty textarea" placeholder="Take A Note" />                                   
                                   <div className="Button"> 
                                   
                                   <div>
                                     
                                    <Icons />
                                     
                                    </div>                                                
                                    <Button className="CloseButton" onClick={this.AddNotes}>close</Button> 
                                   </div>                                                                    
                              </div >
                              : null
                          }
                        </div>
            
                    </CardContent>
                  </Card>                 
              </div>
         
               </ThemeProvider>

          </div>
        )
    }
}