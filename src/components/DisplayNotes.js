import React from 'react'
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import TextareaAutosize from '@material-ui/core/TextareaAutosize';
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
import  '../css/DisplayNotesCSS.css'
import Icons from './Icons'
import TextField from '@material-ui/core/TextField';
import { ThemeProvider, createMuiTheme } from '@material-ui/core'
import EditLocationOutlinedIcon from '@material-ui/icons/EditLocationOutlined';

export default class DisplayNotes extends React.Component{

    constructor(props){
        super(props)
        this.state={
            notes:[],
            notesTitle:'',
        }

        
    }

    handleChange = (event) => {
       
        //   this.notesTitle= event.target.value;
        //   this.setState({
        //             notesTitle: this.notesTitle

        //   })
        this.setState({
            [event.target.name]: event.target.value
        })
         console.log('Notes title', this.state.notesTitle)


        //  this.setState({
        //      notesTitle: event.target.value
        // });
      };
    render(){
        console.log(" print all  notes i display ",this.props.notes);

       var  printNoteList=  this.props.notes.map((item,index)=>{
                return(
                           
                
                                <div id="Small-NotesCardInner" >
          
                                    <Card id="CardIdAllNotes">
                                        <div className="Small-NotesTitleAndDesc">
                                             {/* < EditLocationOutlinedIcon id="pin" />          */}
                                            <TextareaAutosize id="titleId"   name="notesTitle" value={item.notesTitle} placeholder="Title" /> 
                                                     < EditLocationOutlinedIcon  />                 
                                            <TextareaAutosize id="DescriptionId"  name="notesDescription" value={item.notesDescription} placeholder="Description" />
                                        </div>

                                        <div  className="Small-closeButton">
                                            <div>
                                                <Icons noteid={item} />
                                            </div>
                                        </div>
                                    </Card>                 
                                </div>
                           
                    )
                    }
                    )

        return(
            <div className="Small-CardDiv"> 
                {printNoteList}
            </div>
        )
    }
}
