import React, { Component } from 'react'
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import MenuIcon from '@material-ui/icons/Menu';
import Drawer from '@material-ui/core/Drawer';
import SearchIcon from '@material-ui/icons/Search';
import RefreshIcon from '@material-ui/icons/Refresh'
import Settings from '@material-ui/icons/Settings'
import DeleteOutlineOutlinedIcon from '@material-ui/icons/DeleteOutlineOutlined';
import { Button, Label, Divider } from '@material-ui/core';
import List from '@material-ui/core/List';
import NoteOutlinedIcon from '@material-ui/icons/NoteOutlined';
import AddAlertOutlinedIcon from '@material-ui/icons/AddAlertOutlined';
import CheckIcon from '@material-ui/icons/Check';
import IconButton from '@material-ui/core/IconButton';
import InputBase from '@material-ui/core/InputBase';
import Badge from '@material-ui/core/Badge';
import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import EditTwoToneIcon from '@material-ui/icons/EditTwoTone';
import  AxiosService  from '../service/postData';
import '../css/DashBoardCSS.css';
import DisplayNotes from './DisplayNotes'
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { TextField } from '@material-ui/core';
import Labeldata from './Label'
import EditLabel from './EditLabel'
import ArchiveOutlinedIcon from '@material-ui/icons/ArchiveOutlined';


var axiosObject = new AxiosService;
export default class DashBoard extends Component {

    constructor(props){
        super(props);

        this.state = {
            left: false,
            notesInDashBoard:[],
            editlabel:false,
            createlabel:''
        }

        this.onchangeTextField=this.onchangeTextField.bind(this)
        this.AddLabelWithoutId=this.AddLabelWithoutId.bind(this)
        // this.GetNotes = this.GetNotes.bind(this);
        // this.get = this.get.bind(this)
    }

 handleClickOpen = () => {
  this.setState({ editlabel: true })
};

handleClose = () => {
  this.setState({ editlabel: false })
};
    get=() =>
    {
        this.props.history.push('/Dashboard/notes') 
    }

    getArchive=() =>
    {
         this.props.history.push('/Dashboard/ArchiveNotes')
    }

    getTrash=() =>
    {
          this.props.history.push('/Dashboard/TrashNotes')
    }

    AddLabelWithoutId() 
    {
        var data = {               
                    EditLabel: this.state.createlabel,                             
                    }

        axiosObject.AddLabelWithoutNoteService(data).then(response => {
        console.log(" response in ",response); 
        })     
    }

onchangeTextField(e)
{
  this.setState({[e.target.name]: e.target.value});
  console.log("onchange method ",this.state.createlabel);
}


    render() {
                console.log('in dashboard render', this.props.notesInDashBoard);
                 console.log('this is GetNote()', this.state.getAllNotes);
                
       const sideList =
      (
        <div className="DrawersIcon">
          <div className="ListButtons">

             <Button id="reminder-notes-btn" onClick={this.get}>
                <NoteOutlinedIcon id="noteIcon"></NoteOutlinedIcon>
                     Note
             </Button>

            <Button id="reminder-notes-btn" onClick={this.colorBgChange}>
                <AddAlertOutlinedIcon id="noteIcon"></AddAlertOutlinedIcon>
                    Reminders
           </Button>

            <Divider />
            <div>
              <span id="span-label">Labels   </span>
            
              <Labeldata editLabelbool={ this.state.editlabel}/>
           
              <Button id="reminder-notes-btn"  onClick={this.handleClickOpen} >
                <AddAlertOutlinedIcon id="noteIcon"></AddAlertOutlinedIcon>
                Edit Label
         </Button>
     <div>

        <Dialog
          open={this.state.editlabel}
          onClose={this.handleClose}>

          <DialogTitle id="alert-dialog-title">edit Labels</DialogTitle>
          <DialogContent>
           
            <TextField placeholder="create label"  name="createlabel"
              onChange={this.onchangeTextField}/>
           <Button onClick={this.AddLabelWithoutId}>   <CheckIcon /> </Button> 
            <Labeldata labelbool={this.state.editlabel}/>
           <br/>

          </DialogContent>
          <DialogActions>

            <Button onClick={this.handleClose} color="primary" autoFocus>
              Done
          </Button>
          </DialogActions>
        </Dialog>
     </div>


            </div>
            <Divider />
            <Button id="reminder-notes-btn"  onClick={this.getTrash} >
              <DeleteOutlineOutlinedIcon id="noteIcon"></DeleteOutlineOutlinedIcon>
              Trash
      </Button>

            <br />
            <Button id="reminder-notes-btn"  onClick={this.getArchive} >
              <ArchiveOutlinedIcon id="noteIcon"></ArchiveOutlinedIcon>
              Archive
      </Button>
          </div>
        </div>
      );


        return (
            <div className="header-div">
                <AppBar className="AppBar" >
                    <Toolbar>
                        <IconButton  onClick={e => this.setState({ left: !this.state.left })} edge="start" color="inherit" aria-label="menu">
                            <MenuIcon />

                        </IconButton>
                        <Typography variant="h6">
                            Fundoo
                        </Typography>

                        <SearchIcon id="search-icon" />
                        <InputBase placeholder="Search…" className="Search" inputProps={{ 'aria-label': 'search' }} />

                        <div className="RefreshAndSettingIcon">
                            <IconButton color="black" className="left-icon-setting">
                                <Badge color="secondary">
                                    <RefreshIcon />
                                </Badge>
                            </IconButton>

                            <IconButton color="black" className="left-icon-setting">
                                <Badge color="secondary">
                                    <Settings />
                                </Badge>
                            </IconButton>

                        </div>
                    </Toolbar>
                </AppBar>
                 <Divider />

                <div>
                    <Drawer
                        variant="persistent"
                        open={this.state.left}
                        onOpen={e => this.setState({ left: false })} >
                        <div style={{ width: "200px"} }>
                            {sideList}
                        </div>
                    </Drawer>

                </div>
                 <div>
                        <DisplayNotes notes={this.state.notesInDashBoard} />         

                 </div>   
                
            </div>

            
        );
    }
}

