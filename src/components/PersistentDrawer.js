import React, { Component } from 'react';
import clsx from 'clsx';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import CssBaseline from '@material-ui/core/CssBaseline';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import InboxIcon from '@material-ui/icons/MoveToInbox';
import MailIcon from '@material-ui/icons/Mail';
import { Button } from '@material-ui/core';
import NoteOutlinedIcon from '@material-ui/icons/NoteOutlined';
import AddAlertOutlinedIcon from '@material-ui/icons/AddAlertOutlined';
import DeleteOutlineOutlinedIcon from '@material-ui/icons/DeleteOutlineOutlined';
import ArchiveOutlinedIcon from '@material-ui/icons/ArchiveOutlined';
import { ThemeProvider ,createMuiTheme} from '@material-ui/core'   
const drawerWidth = 240;


const theme = createMuiTheme({
   
    overrides:{
        MuiDrawer:{
            paper:{
                top:"63px"
            }
        },


    }
  });

export  class PersistentDrawer extends Component {
constructor()
{
    super();
    this.state={
     left:false
    }
}

handleDrawerOpen = () => {
    this.setState({
      left: !this.state.left,
    });
  };
  
render()
{

  return (
      <div>
    <div >
      <CssBaseline />
      <AppBar
        position="fixed"
      
      >
        <Toolbar>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            onClick={this.handleDrawerOpen}
            edge="start"
           
          >
            <MenuIcon />
          </IconButton>
                 
        </Toolbar>
      </AppBar>
      </div>
     
      <div>
      <ThemeProvider theme={theme}>
      <Drawer
      
        variant="persistent"
        anchor="left"
        open={this.state.left}
      >
       
        <Divider />
        <List>
          <div className="ListButtons">
     <Button id="reminder-notes-btn">
     <NoteOutlinedIcon id= "noteIcon"></NoteOutlinedIcon>
      Note
     </Button>
     <br />

       <Button id="reminder-notes-btn"   >
       <AddAlertOutlinedIcon id= "noteIcon"></AddAlertOutlinedIcon>
        Reminders
       </Button>
       <br />

       <Button id="reminder-notes-btn"   >
       <DeleteOutlineOutlinedIcon id= "noteIcon"></DeleteOutlineOutlinedIcon>
        Trash
      </Button>

       <br />
       <Button id="reminder-notes-btn"   >
       <ArchiveOutlinedIcon id= "noteIcon"></ArchiveOutlinedIcon>
        Archive
      </Button>
     </div>
        </List>
        <Divider />
        <List>
          {['All mail', 'Trash', 'Spam'].map((text, index) => (
            <ListItem button key={text}>
              <ListItemIcon>{index % 2 === 0 ? <InboxIcon /> : <MailIcon />}</ListItemIcon>
              <ListItemText primary={text} />
            </ListItem>
          ))}
        </List>
      </Drawer>
      </ThemeProvider>
    </div>
   
    </div>
  );
}}
