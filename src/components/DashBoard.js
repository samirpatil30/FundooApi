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
import ArchiveOutlinedIcon from '@material-ui/icons/ArchiveOutlined';
import IconButton from '@material-ui/core/IconButton';
import InputBase from '@material-ui/core/InputBase';
import Badge from '@material-ui/core/Badge';
import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import EditTwoToneIcon from '@material-ui/icons/EditTwoTone';

import '../css/DashBoardCSS.css';

export default class DashBoard extends Component {

    constructor(props){
        super(props);

        this.state = {
            left: true,

        }
    }

    render() {
                console.log('in dashboard');
                
        const sideList =
            (

                <div className="DrawersIcon">
                    <div className="ListButtons">
                         <Button id="reminder-notes-btn" >
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
                            <br />  <br />  <br />
                            <Button id="reminder-notes-btn"   >
                            <EditTwoToneIcon id="noteIcon"></EditTwoToneIcon>
                              Edit Label
                            </Button>
                          </div>
                          <Divider />

                          <Button id="reminder-notes-btn"   >
                          <DeleteOutlineOutlinedIcon id="noteIcon"></DeleteOutlineOutlinedIcon>
                            Trash
                          </Button>
                          <br />
                          <Button id="reminder-notes-btn"   >
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
                            <MenuIcon
                               
                            />
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

                
            </div>

            
        );
    }
}

