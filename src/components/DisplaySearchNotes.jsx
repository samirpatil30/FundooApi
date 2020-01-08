import React from "react";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import TextareaAutosize from "@material-ui/core/TextareaAutosize";
import Button from "@material-ui/core/Button";
import Tooltip from "@material-ui/core/Tooltip";
import ReminderIcon from "@material-ui/icons/AddAlertOutlined";
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
import InputBase from "@material-ui/core/InputBase";
import Icons from "./Icons";
import { MuiThemeProvider, createMuiTheme } from "@material-ui/core";
import TextField from "@material-ui/core/TextField";
import EditLocationOutlinedIcon from "@material-ui/icons/EditLocationOutlined";
import ColorComponent from "./ColorCompnent";
import Dialog from "@material-ui/core/Dialog";
import "../css/UpdateCardCSS.css";
import Avatar from "@material-ui/core/Avatar";
import NewCollabrator from "./NewCollabrator";
import ArchiveComponent from "./ArchiveComponent";
import NewReminder from "./NewReminder";
import AxiosService from "../service/postData";
import UpdateDialog from './UpdateDialog'
import Image from "./Image";
import UntrashNotes from "./UntrashNotes";


const theme = createMuiTheme({
  overrides: {
    MuiBackdrop: {
      root: {
        backgroundColor: "transperent"
      }
    }
  }
});

var axiosObject = new AxiosService();



export default class DisplaySearchNotes extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      notes: [],
      showUpdateNotesCard: false,
      Title: "dasvfs",
      Description: "dsvds",
      color: "",
      NoteImage:"",
      noteId:''
    };
  }

  handleChange = e => {
    console.log('this is handlechange', e.target.value);

    this.setState({ [e.target.name]: e.target.value });
    
  };

  operation = async note => {
    await this.setState({
      showUpdateNotesCard: true,
      Title: note.notesTitle,
      Description: note.notesDescription,
      color: note.color,
      NoteImage: note.image,
      noteId: note.id,
      userId: note.UserId
    
    });
    // this.setState({Title:title})
  };

  handleSave = () => {
    this.props.getMethod();
  };

   handleArchiveNotes = () => {
    this.props.GetArchiveMethod();
  };

  handleTrashNotes =() =>
  {
    this.props.getAllTrashNotes();
  }

  UpdateNote = () => {
    this.setState({
      showUpdateNotesCard: false
    });
    var data = {
      DialogNoteTitle: this.state.DialogNoteTitle,
      DialogDescription: this.state.DialogDescription,
      color: this.state.color
    };

    axiosObject.UpdateNoteService(data);
  };

  componentMount=() => {
    this.props.getMethod;
  }

  render() {
    console.log(" IDDD");

    var printNoteList = this.props.Searchnotes.map((item, index) => {
      return (
        <div id="Small-NotesCardInner">
          {/* {
              this.props.

          } */}
          <Card key={item.noteid} id="CardIdAllNotes" style={{ backgroundColor: item.color }}>

            <div>
              <div
                className="Small-NotesTitleAndDesc"
                style={{ backgroundColor: item.color }}
              >
                <TextareaAutosize
                  style={{ backgroundColor: item.color }}
                  id="titleId"
                  name="notesTitle"
                  placeholder="Title"
                  onClick={() => this.operation(item)}
                  value={item.notesTitle}
                />

                <TextareaAutosize
                  style={{ backgroundColor: item.color }}
                  id="DescriptionId"
                  name="notesDescription"
                   value={item.notesDescription}
                  placeholder="Description" />
               
              </div>

              <div className="Small-closeButton">
               {
                  item.trash === true ? <UntrashNotes noteid={item} /> : 

                  <div className="Small-closeButton">
                  <NewReminder noteid={item.id} /> 
                <NewCollabrator idItem={item.id} />
                <ArchiveComponent noteid={item} getNoteMethod={this.handleSave} />
                <Image noteId={item.id}/>
                <ColorComponent
                  noteId={item.id}
                  ReturnColor={item.color}
                  getNoteMethod={this.handleSave}
                />
                <Icons noteid={item} getNoteMethod={this.handleSave} />
                 </div>
               }
                
              </div>
            </div>
          </Card>
        </div>
      );
    });
 

    return <div className="Small-CardDiv">  {printNoteList}
     {
       this.state.showUpdateNotesCard === true ?
       <div>
          <UpdateDialog Title={this.state.Title}
          Description= {this.state.Description}
          noteId = {this.state.noteId}
          userId= {this.state.userId}
          color= {this.state.color}
          NoteImage= {this.state.NoteImage}
          GetMethod= {this.handleSave}
          />
          </div>
        : null
     }
    
    
    </div>;
  }
}
