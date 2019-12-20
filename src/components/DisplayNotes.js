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
import { ThemeProvider, createMuiTheme } from '@material-ui/core'


export class DisplayNotes extends React.Component{

    constructor(props){
        super(props)
        this.state={
            notes:[]
        }
    }
    render(){
        console.log(" print all  notes i display ",this.props.notes);

       var  printNoteList=  this.props.notes.map((item,index)=>{
                return(
                <div key={index} className="CradDiv">
                     <Card className="card">
                            <CardContent id="card-content">   
                            <div>
                                <div className="TextFieldTitle">
                    
                                    <TextareaAutosize 
                                    className="DisplayNotsTitle" 
                                    name="notesTitle" 
                                    onChange={this.onchange} 
                                    onClick={this.operation} 
                                    placeholder="Title"
                                    value={item.notesTitle} />
                      
                                </div>
                                    <div className="TextField2">
                                        <TextareaAutosize className="note-text-area" 
                                        name="notesDescription" 
                                        onChange={this.onchange} 
                                        aria-multiline="true" 
                                        aria-label="empty textarea" 
                                        placeholder="Take A Note"
                                        value={item.notesDescription} />
                                </div>
                                    <div className="IconDiv">                    
                                          <Icons noteid={item} />
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
