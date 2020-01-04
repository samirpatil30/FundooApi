import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { Component } from 'react';
import InputBase from '@material-ui/core/InputBase';
import ColorCompnent from './ColorCompnent'
import NewCollabrator from './NewCollabrator'
import Labeldata from './Label'
// import ArchiveComponent from "./ArchiveComponent";
import NewReminder from './NewReminder'
import "../css/UpdateCardCSS.css";
import  AxiosService  from '../service/postData';

 var updatenote = new AxiosService();
export default class UpdateDialog extends Component {
 constructor(props){
   super(props)
   this.state={
    setOpen:true,
    open:true,
    title:this.props.Title,
    description:this.props.Description,
    noteId:this.props.noteId,
    userId:this.props.userId,
    color: this.props.color,
    NoteImage: this.props.NoteImage
   }
   this.updatenoteData =this.updatenoteData.bind(this)
 }


  onchangeInput = (e) =>{
    this.setState({[e.target.name]: e.target.value});
  }
  //  handleClose = () => {
  //   this.setState({
  //     setOpen:false,
  //     open:false
  //   })
  //   this.updatenoteData();
  // };

  updatenoteData()
  {
    this.setState({
      setOpen:false,
      open:false
    })
    var data = {
                 
      NotesTitle: this.state.title,                             
      NotesDescription : this.state.description,
      noteId:this.state.noteId,
      color:this.state.color,
      NoteImage: this.state.NoteImage

    }

    updatenote.UpdateNoteService(data).then(response=>{
        console.log("Update Notesss response in ",response);
          this.props.GetMethod();
      })
  }
render(){
console.log("Idddd of notes ",this.state.noteId);


  return (
    <div>
       
      <Dialog
        open={this.state.open}
        onClose={this.handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
       
      >
        <div className="Image">
                 {
                   <img  src= {this.state.NoteImage}  alt="Noimage"/> 
                 }
                   
            </div>
        <DialogContent  style={{ backgroundColor: this.state.color }}>
        
        <InputBase
       
        fullWidth
        multiline="true"
        className=""
        value={this.state.title}
        onChange={this.onchangeInput}
         name="title"
       placeholder="Title"
        inputProps={{ 'aria-label': 'naked' }}
      />
      <br/>
      <InputBase
        className="" 
        fullWidth
        name="description"
        multiline="true"
        value={this.state.description}
        onChange={this.onchangeInput}
        placeholder="Description"
        inputProps={{ 'aria-label': 'naked' }}
      />
      <div id="IconComponent"  style={{ backgroundColor: this.state.color }}>
      <NewCollabrator idItem={this.state.noteId} />
      {/* <ArchiveComponent /> */}
      <ColorCompnent />
        <NewReminder />

      <Button  color="primary" autoFocus onClick={this.updatenoteData}> CLOSE </Button>

      </div>
 
        </DialogContent>
        <DialogActions>       
        </DialogActions>
      </Dialog>
    </div>
  );
}
}