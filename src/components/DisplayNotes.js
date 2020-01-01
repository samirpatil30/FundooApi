import React from "react";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import TextareaAutosize from "@material-ui/core/TextareaAutosize";
import Button from "@material-ui/core/Button";
import Tooltip from "@material-ui/core/Tooltip";
import ReminderIcon from '@material-ui/icons/AddAlertOutlined';
import AccessAlarmsIcon from "@material-ui/icons/AccessAlarms";
import PersonAddIcon from "@material-ui/icons/PersonAdd";
import PaletteIcon from "@material-ui/icons/Palette";
import ArchiveIcon from "@material-ui/icons/Archive";
import UnarchiveIcon from "@material-ui/icons/Unarchive";
import MoreVertIcon from "@material-ui/icons/MoreVert";
import UndoIcon from "@material-ui/icons/Undo";
import RedoIcon from "@material-ui/icons/Redo";
import IconButton from "@material-ui/core/IconButton";
import Badge from "@material-ui/core/Badge";
import "../css/DisplayNotesCSS.css";
import Icons from "./Icons";
import { MuiThemeProvider, createMuiTheme } from "@material-ui/core";
import TextField from "@material-ui/core/TextField";
import EditLocationOutlinedIcon from "@material-ui/icons/EditLocationOutlined";
import ColorComponent from './ColorCompnent'
import Dialog from "@material-ui/core/Dialog";
import "../css/UpdateCardCSS.css";
import Avatar from '@material-ui/core/Avatar';
import NewCollabrator from "./NewCollabrator";
import ArchiveComponent from "./ArchiveComponent";
import NewReminder from "./NewReminder";

const theme = createMuiTheme({
  overrides: {
    MuiBackdrop: {
      root: {
        backgroundColor: "transperent"
      }
    },
  }
});

export default class DisplayNotes extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      notes:[],
      showUpdateNotesCard: false,
      Title:"dasvfs",
      Description:"dsvds",
      color:''
    };
  }

  operation= async (note) =>{
         await   this.setState({
        showUpdateNotesCard: true,    
          Title: note.notesTitle,
          Description: note.notesDescription,
          color: note.color 
     });
    // this.setState({Title:title})
    console.log("status of operations",this.state.Title,this.state.Description);
  };

  operationHide=() =>  {
    this.setState({
      showUpdateNotesCard: false
    });
  };

  handleSave=() => {
    this.props.getMethod()
  }

  componentDidMount() {
  
   this.props.getMethod
  }

  render() {

    console.log(" print all  notes i display Notesss ", this.props.notes);
      console.log('notessTitle', this.state.title);
      
    var printNoteList = this.props.notes.map((item, index) => {
       return  (

        <div id="Small-NotesCardInner">
          <Card id="CardIdAllNotes"   style={{backgroundColor:item.color}} >
            
              <div >
                <div className="Small-NotesTitleAndDesc" style={{backgroundColor:item.color}}>
                  <TextareaAutosize
                  style={{backgroundColor:item.color}}
                    id="titleId"
                    name="notesTitle" 
                    placeholder="Title"
                    onClick={()=>this.operation(item)}
                    value={item.notesTitle}
                  />
               
                  <TextareaAutosize 
                  style={{backgroundColor:item.color}}
                    id="DescriptionId"
                    name="notesDescription"
                    value={item.notesDescription}
                    placeholder="Description"
                  />
                  {/* <Avatar src="./public/01.jpg"/> */}
                </div>
                 
                <div className="Small-closeButton">
                <NewReminder noteid={item.id} />
                  <NewCollabrator  idItem={item.id} />
                  <ArchiveComponent noteid={item} />
                  <ColorComponent noteId={item.id} ReturnColor={item.color} getNoteMethod= {this.handleSave} />
                  <Icons noteid={item} getNoteMethod= {this.handleSave}/>
                  {/* <MoreComponent  noteid={item.id} /> */}
                </div>
              </div>
          </Card>

           <div id="Update-Notes">
          <MuiThemeProvider theme={theme}>
          
            <Dialog id="Dialog"  open={this.state.showUpdateNotesCard}   >
              <div id="Update-UpdateNotesCardInner"  style={{backgroundColor:this.state.color}}>
                                    
                  <div  style={{backgroundColor:item.color}}>
                    <div className="Update-NotesTitleAndDesc"  style={{backgroundColor:this.state.color}}>
                      <TextareaAutosize 
                        id="UpdateNotetitleId"
                        name="notesTitle"
                        value={this.state.Title}
                        placeholder="NoteTitle"
                        onClick={this.operation}
                         style={{backgroundColor:this.state.color}}
                      />
                      <EditLocationOutlinedIcon />
                      <TextareaAutosize
                        id="UpdateNoteDescriptionId"
                        name="notesDescription"
                        value={this.state.Description}
                        placeholder="Description"
                         style={{backgroundColor:this.state.color}}
                      />
                    </div>

                    <div className="Small-closeButton">
                      
                <div className="Small-closeButton"  style={{backgroundColor:this.state.color}}>
                  <NewReminder noteid={item.id} />
                  <NewCollabrator  idItem={item.id} />
                  <ArchiveComponent noteid={item} />
                  <ColorComponent noteId={item.id} ReturnColor={item.color} getNoteMethod= {this.handleSave} />
                  <Icons noteid={item} getNoteMethod= {this.handleSave}/>
                  {/* <MoreComponent  noteid={item.id} /> */}
                </div>
                    
                     
                      <Button onClick={this.operationHide} style={{backgroundColor:this.state.color}}>Close</Button>
                    </div>
                  </div>
              </div>
            </Dialog>
          </MuiThemeProvider>
        </div>

        </div>
      );
    });
    console.log('in render', printNoteList);
    

    return( <div className="Small-CardDiv">{printNoteList}</div> );
  }
}
