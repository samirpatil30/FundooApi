import React from "react";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import TextareaAutosize from "@material-ui/core/TextareaAutosize";
import Button from "@material-ui/core/Button";
import Tooltip from "@material-ui/core/Tooltip";
import AddAlertTwoToneIcon from "@material-ui/icons/AddAlertTwoTone";
import AccessAlarmsIcon from "@material-ui/icons/AccessAlarms";
import PersonAddIcon from "@material-ui/icons/PersonAdd";
import PaletteIcon from "@material-ui/icons/Palette";
import ImageIcon from "@material-ui/icons/Image";
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
import Dialog from "@material-ui/core/Dialog";
import "../css/UpdateCardCSS.css";

const theme = createMuiTheme({
  overrides: {
    MuiBackdrop: {
      root: {
        backgroundColor: "transperent"
      }
    },

    MuiPaper: {
      elevation24: {
        boxShadow:
          "0px 0px 0px 0px rgba(0,0,0,0.2), 0px 2px 7px 1px rgba(0,0,0,0.14), 0px 0px 1px 0px rgba(0,0,0,0.12)"
      }
    }
  }
});

export default class DisplayNotes extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      notes:[],
      showUpdateNotesCard: false,
      Title:"dasvfs",
      Description:"dsvds"
    };
    
   
  }

  operation= async (note) =>{
         await   this.setState({
        showUpdateNotesCard: true,    
          Title: note.notesTitle,
          Description: note.notesDescription 
     });
    // this.setState({Title:title})
    console.log("status of operations",this.state.Title,this.state.Description);
  };

  operationHide()  {
    this.setState({
      showUpdateNotesCard: false
    });
  };

  render() {

    console.log(" print all  notes i display Notesss ", this.props.notes);
      console.log('notessTitle', this.state.title);
      
    var printNoteList = this.props.notes.map((item, index) => {
       return  (

        // <div id="Update-Notes">
        //   <MuiThemeProvider theme={theme}>
        //     <Dialog id="Dialog"  open={this.state.showUpdateNotesCard}  >
             
        //       <div id="Update-UpdateNotesCardInner">
               
        //           <div>
        //             <div className="Update-NotesTitleAndDesc">
        //               <TextareaAutosize 
        //                 id="UpdateNotetitleId"
        //                 name="notesTitle"
        //                 value={this.state.Title}
        //                 placeholder="NoteTitle"
        //                 onClick={this.operation}
        //               />
        //               <EditLocationOutlinedIcon />
        //               <TextareaAutosize
        //                 id="UpdateNoteDescriptionId"
        //                 name="notesDescription"
        //                 value={this.state.Description}
        //                 placeholder="Description"
        //               />
        //             </div>

        //             <div className="Small-closeButton">
        //               <Icons noteid={item} />
        //               <Button>Close</Button>
        //             </div>
        //           </div>
              
        //       </div>
        //     </Dialog>
        //   </MuiThemeProvider>
        // </div>
      // ) 
      
      
      // (
        <div id="Small-NotesCardInner">
          <Card id="CardIdAllNotes" onClick={()=>this.operation(item)}  >
            
              <div >
                <div className="Small-NotesTitleAndDesc">
                  <TextareaAutosize
                    id="titleId"
                    name="notesTitle"
                    value={item.notesTitle}
                    placeholder="Title"
                    onClick={()=>this.operation(item.notesTitle, item.notesDescription)}
                  />
                  <EditLocationOutlinedIcon />
                  <TextareaAutosize 
                    id="DescriptionId"
                    name="notesDescription"
                    value={item.notesDescription}
                    placeholder="Description"
                  />
                </div>
                  {/* <div>
                    <h2> {item.color} </h2>
                  </div> */}
                <div className="Small-closeButton">
                  <Icons noteid={item} />
                </div>
              </div>
            
          </Card>

           <div id="Update-Notes">
          <MuiThemeProvider theme={theme}>
            <Dialog id="Dialog"  open={this.state.showUpdateNotesCard}  >
             
              <div id="Update-UpdateNotesCardInner">
               
                  <div>
                    <div className="Update-NotesTitleAndDesc">
                      <TextareaAutosize 
                        id="UpdateNotetitleId"
                        name="notesTitle"
                        value={this.state.Title}
                        placeholder="NoteTitle"
                        onClick={this.operation}
                      />
                      <EditLocationOutlinedIcon />
                      <TextareaAutosize
                        id="UpdateNoteDescriptionId"
                        name="notesDescription"
                        value={this.state.Description}
                        placeholder="Description"
                      />
                    </div>

                    <div className="Small-closeButton">
                      <Icons noteid={item} />
                      <Button>Close</Button>
                    </div>
                  </div>
              
              </div>
            </Dialog>
          </MuiThemeProvider>
        </div>

        </div>
      );
    });
    console.log('in render',printNoteList);
    

    return( <div className="Small-CardDiv">{printNoteList}</div> );
  }
}
