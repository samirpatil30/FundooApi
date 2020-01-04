import React, { Component } from 'react';
import Button from "@material-ui/core/Button";
import Popover from '@material-ui/core/Popover';
import MenuItem from '@material-ui/core/MenuItem';
import MenuList from '@material-ui/core/MenuList';
import TextField from '@material-ui/core/TextField';
import Tooltip from '@material-ui/core/Tooltip';
import { createMuiTheme, MuiThemeProvider } from "@material-ui/core";
import AccessTimeIcon from '@material-ui/icons/AccessTime';
import AddAlertOutlinedIcon from "@material-ui/icons/AddAlertOutlined";
import '../css/Reminder.css'
import AxiosService from "../service/postData";

function titleDesSearch(searchWord) {
    return function (x) {
        return x.title.includes(SearchText) || x.description.includes(SearchText)
    }
}
var axiosObject = new AxiosService;
export default class NewReminder extends React.Component {
    state = {
        anchorEl: null,
        open: false,
        openChildMenu: false,
        noteLabels: [],
        userReminderDate: "",
        userReminderTime:"",
        userDateTime:"",
        date:[]
    };

   
    handleClick = event => {
        this.setState({
            anchorEl: event.currentTarget, 
            open: true
        });
    };

   
    handleClose = () => {
        this.setState({ anchorEl: null, open: false, 
        openChildMenu: false });
    };

    handleChildMenu = () => {
        this.setState({ open: false, 
        openChildMenu: true });
    }

    handleDate = (event) => {
        this.setState({ userReminderDate:event.target.value});
        
    }

    handleTime = (event) => {
        this.setState({ userReminderTime: event.target.value });
        console.log(this.state.userReminderTime);
        
    }

     AddReminder = (requestValue) => {
        this.setState({ anchorEl: null, open: false, openChildMenu: false });
        var today = new Date();
        let day = today.getDate(); 
        let month = today.getMonth();
        let year = today.getFullYear();

        var reminderDate; 

      
        if (requestValue === 1) {
            reminderDate = new Date();
             reminderDate
            // var newDate = reminderDate.valueOf()
            console.log("today date1", reminderDate);
             
             console.log("today idd", this.props.noteid);

            var data={
                Reminder: reminderDate,
                Id: this.props.noteid
            }    

              axiosObject.AddReminderService(data).then(response =>{
             console.log(response);
            })
          
        } 
        else if (requestValue === 2) {
            var tomorrow = new Date();
            tomorrow.setDate(today.getDate() + 1);
            tomorrow.setHours(8);
            tomorrow.setMinutes(0);
            tomorrow.setSeconds(0);
            tomorrow.setMilliseconds(0);
            reminderDate = tomorrow
            console.log("tomorrow date ", reminderDate);

             this.setState({
                Reminder: reminderDate
            })

             var data={
                Reminder: reminderDate,
                Id: this.props.noteid
            }    

              axiosObject.AddReminderService(data).then(response =>{
             console.log(response);
            })

        } 

        else {
            let concatDate = this.state.userReminderDate + " " + this.state.userReminderTime;            
            let newDate = new Date(concatDate)
            reminderDate = newDate;

               var data={
                   
               } 
             axiosObject.AddReminderService(data).then(response =>{
             console.log(response);
            })       

        }
        console.log("note data",this.props.noteid);
        
        if (this.props.noteid) {
            let remindernew=''

            date=reminderDate.split(' ')

            console.log(date,'date array');
            
            remindernew=date[2]+' '+date[1]+' '+date[3]+' '+date[4]
            console.log("new date",remindernew);    
           
           
             axiosObject.AddReminderService(remindernew).then(response => {
             console.log(response);
            })
        } 
        else {
            // this.props.setReminderOnNewNote(reminderDate);
            let date=[]
            date=reminderDate.split(' ')
            console.log(date,'date array');
            let remindernew=''
            remindernew = date[2]+' '+date[1]+' '+date[3]+' '+date[4]+' UTC'
            console.log("note data",this.props.noteid, remindernew);
          
        }
    }
    
    
    render() {
      console.log('Reminder idddddd', this.props.noteid);
      
        const { anchorEl } = this.state;

        return (
            <div>
                <Tooltip title="reminder">
                  <AddAlertOutlinedIcon fontSize="inherit"
                    style={{ width: "20px" }}
                    onClick={this.handleClick}
                  />
                </Tooltip>

              <div>
                    <Popover
                        open={this.state.open}
                        anchorEl={anchorEl}
                        onClose={this.handleClose}
                        style={{ width: '40%' }} >
                        <div>
                            <div className="reminderTextStyle">
                                <label>Reminder:</label>
                            </div>
                            <MenuList>

                                <MenuItem onClick={() => this.AddReminder(1)}>
                                    <div className="reminderStyle">
                                        Later today <span>8 PM</span>
                                    </div>
                                </MenuItem>

                                <MenuItem onClick={() => this.AddReminder(2)}>
                                    <div className="reminderStyle">
                                        Tomorrow <span>8 AM</span>
                                    </div>
                                </MenuItem>

                                <div className="watchReminderStyle">
                                    <MenuItem onClick={this.handleChildMenu}><AccessTimeIcon font-size="inherit"/>  <h6 id="size">Pick date & time</h6></MenuItem>
                                </div>
                            </MenuList>
                        </div>
                    </Popover>
                </div>

               
                    <Popover
                        open={this.state.openChildMenu}
                        anchorEl={anchorEl}
                        onClose={this.handleClose}>
                      
                            <MenuList>
                                <div className="dateTimeStyle">
                                <TextField
                                    label="Date"
                                    type="date"
                                    value={this.state.userReminderDate}
                                    onChange={this.handleDate}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                />
                                <TextField
                                    label="Time"
                                    type="time"
                                    value={this.state.userReminderTime}
                                    onChange={this.handleTime}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                />
                                <Button variant="contained"
                                    size="large"
                                  
                                    onClick={this.AddReminder}>save</Button>
                                
                                </div>
                        </MenuList>
                    </Popover>
            </div>
        )
    }
}