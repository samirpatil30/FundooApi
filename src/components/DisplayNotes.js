import React from 'react'
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import TextareaAutosize from '@material-ui/core/TextareaAutosize';
import '../css/DisplayNotesCSS.css'
import Button from '@material-ui/core/Button';
import Tooltip from '@material-ui/core/Tooltip';
import AddAlertTwoToneIcon from '@material-ui/icons/AddAlertTwoTone';
import AccessAlarmsIcon from '@material-ui/icons/AccessAlarms';
import PersonAddIcon from '@material-ui/icons/PersonAdd';
import PaletteIcon from '@material-ui/icons/Palette';
import ImageIcon from '@material-ui/icons/Image';
import ArchiveIcon from '@material-ui/icons/Archive';
import UnarchiveIcon from '@material-ui/icons/Unarchive';
import MoreVertIcon from '@material-ui/icons/MoreVert';
import UndoIcon from '@material-ui/icons/Undo';
import RedoIcon from '@material-ui/icons/Redo';
import IconButton from '@material-ui/core/IconButton';
import Badge from '@material-ui/core/Badge';
import '../css/CardNotes.css'
import Notes from './Notes'
import { ThemeProvider, createMuiTheme } from '@material-ui/core'
const theme = createMuiTheme({


    overrides: {

        MuiPaper: {
            rounded: {
                width: "220px",
                borderRadius: "10px"

            }
        },
        MuiSvgIcon: {
            root: {

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

export class DisplayNotes extends React.Component{

    constructor(props){
        super(props)
        this.state={
            notes:[]
        }
    }
    render(){
        console.log(" print all  notes i display ",this.props.notes);
        

       var  printNoteList=  this.props.notes.map( (key)=>{
        //    console.log(" key ",key.title);
           
                return(
                    <div>
                            <Card className="card-class">
                <CardContent id="card-content">   
                <div className="TextFieldTitle">
                    
                    <TextareaAutosize className="title-text-area" name="notesTitle" value={key.notesTitle} onChange={this.onchange} onClick={this.operation} placeholder="Title" />
                      
                       
                        <div className="TextField2">
                              <TextareaAutosize className="take-note-text-area" value= {key.notesDescription} name="notesDescription" onChange={this.onchange} aria-multiline="true" aria-label="empty textarea" placeholder="Take A Note" />
                              <Tooltip title="Reminder" enterDelay={250} leaveDelay={10}>
                              <IconButton color="black">
                              <Badge  color="secondary">
                              < AccessAlarmsIcon precision={1} className="bottom-icon-list"/>
                              </Badge>
                              </IconButton>
                              </Tooltip>

                              <Tooltip title="Collaborate" enterDelay={250} leaveDelay={100}>
                              <IconButton color="black">
                              <Badge  color="secondary">
                              <PersonAddIcon  className="bottom-icon-list"/>
                              </Badge>
                              </IconButton>
                              </Tooltip>

                               <Tooltip title="Color" enterDelay={250} leaveDelay={100}>
                               <IconButton color="black">
                                <Badge  color="secondary">
                                <PaletteIcon  className="bottom-icon-list"/>
                                </Badge>
                                </IconButton>
                                </Tooltip>
                
                                <Tooltip title="Image" enterDelay={250} leaveDelay={100}>
                                <IconButton color="black">
                                <Badge  color="secondary">
                                <ImageIcon  className="bottom-icon-list"/>
                                </Badge>
                                </IconButton>
                                </Tooltip>

                                <Tooltip title="Archive" enterDelay={250} leaveDelay={100}>
                                <IconButton color="black">
                                 <Badge  color="secondary">
                                <ArchiveIcon  className="bottom-icon-list"/>   
                                </Badge>
                                </IconButton>
                                </Tooltip>

            
                                  <Tooltip title="More" enterDelay={250} leaveDelay={100}>
                                  <IconButton color="black" onClick={this.handleClick}>
                                  <Badge  color="secondary">
                                  <MoreVertIcon  className="bottom-icon-list"/>
                                  </Badge>
                                  </IconButton>
                                  </Tooltip>      
                                     
                                  <Button className="CloseButton" onClick={this.AddNotes}>close</Button>  
                                  
                        </div>
                     
             
                </div>
            
               </CardContent>
          
               </Card>               
                    </div>
                    )
                    })
        return(
            <div>
                {printNoteList}
            </div>
        )
    }
}
