
import React from "react";
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import Popper from '@material-ui/core/Popper';
import PopupState, { bindToggle, bindPopper } from 'material-ui-popup-state';
import Fade from '@material-ui/core/Fade';
import Paper from '@material-ui/core/Paper';
import { Component } from 'react';
import '../css/ColorComponentCSS.css'
import RadioButtonUncheckedOutlinedIcon from '@material-ui/icons/RadioButtonUncheckedOutlined';
import { IconButton } from "@material-ui/core";
import Icons from './Icons'
import DisplayNotes from "./DisplayNotes";
import PaletteIcon from '@material-ui/icons/Palette';
import AxiosService from '../service/postData'
import Badge from '@material-ui/core/Badge';
import Tooltip from '@material-ui/core/Tooltip';


var axiosObject = new AxiosService;
export default class ChangeColor extends Component
{
    constructor(props)
    {
        super(props);
        this.state={
            bg: "",
            showColorMenu:false
        }
        this.colorChange = this.colorChange.bind(this)
    }

    

    DisplayColors = ()=>{
      this.setState({
        showColorMenu: !this.state.showColorMenu
      })
    }
    
    changeCSS(clr) {
        console.log("Color ",clr);
        
        this.setState({
          bg: clr
        });
      };
    
     
    async  colorChange(clr) {
        console.log("This is funtion")

      await  this.setState({
          bg: clr
        });
          var data = {
                 
            color: this.state.bg,                             
            Id : this.props.noteId,       
          }
         
             axiosObject.GetColorService(data).then(response=>{
              console.log(" response in ",response);
             this.props.getNoteMethod()
              
            })            
    }
    render(){
      console.log("color value ",this.state.showColorMenu);
      
        return(
            <div>


<PopupState variant="popper" popupId="demo-popup-popper">
      {popupState => (
        <div>
        

          <Tooltip title="Color" enterDelay={250} leaveDelay={100}>
             <IconButton color="black" {...bindToggle(popupState)} >
             <Badge color="secondary">
              <PaletteIcon />
            </Badge>
           </IconButton>
           </Tooltip>

          <Popper {...bindPopper(popupState)} transition>
            {({ TransitionProps }) => (
              <Fade {...TransitionProps} timeout={350}>
                <Paper>
                <div className="allcolorButtonDiv">
                      <Paper id="paperContent">                      
                    <IconButton id="redColor" onClick={this.colorChange.bind(this, "e7baba")}/>
                    <IconButton id="blueColor"  onClick={this.colorChange.bind(this, "a94afc")}/>
                    <IconButton id="blackColor" onClick={this.colorChange.bind(this, "d7aefb")}/>
                    <IconButton id="greenColor" onClick={this.colorChange.bind(this, "79e47e")}/>
                    
                    <br/>
                    <IconButton id="pinkColor"  onClick={this.colorChange.bind(this, "ffffff")}/>
                    <IconButton id="yellowColor"  onClick={this.colorChange.bind(this, "99929c")}/>
                    <IconButton id="brownColor"  onClick={this.colorChange.bind(this, "ad5071")}/>
                    <IconButton id="grayColor"  onClick={this.colorChange.bind(this, "af2a9f")}/>
                    <br/>
                    <IconButton id="purpleColor"   onClick={this.colorChange.bind(this, "15c7f3")}/>
                    <IconButton id="darkBlueColor"   onClick={this.colorChange.bind(this, "cc6931")}/>
                    <IconButton id="oragneColor"   onClick={this.colorChange.bind(this, "cad118")}/>
                    <IconButton id="whiteColor"   onClick={this.colorChange.bind(this, "5ad6d6")}/>
                      </Paper>
                  


              
              </div>
                </Paper>
              </Fade>
            )}
          </Popper>
        </div>
      )}
    </PopupState>

                         
        </div> 
        )
    }
}
