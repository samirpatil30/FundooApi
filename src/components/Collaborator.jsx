import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Avatar from '@material-ui/core/Avatar';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import ListItemText from '@material-ui/core/ListItemText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Dialog from '@material-ui/core/Dialog';
import PersonIcon from '@material-ui/icons/Person';
import AddIcon from '@material-ui/icons/Add';
import Typography from '@material-ui/core/Typography';
import blue from '@material-ui/core/colors/blue';
import jwt_decode from 'jwt-decode'
import { TextField, Divider } from '@material-ui/core';
import '../css/CollabratorCSS.css';
import AddCircleIcon from '@material-ui/icons/AddCircle';
import AddCollabarate from './AddCollabarate'


var JwtToken = localStorage.getItem('Token')
var decoded = jwt_decode(JwtToken)
var emailOwner = decoded.Email+"      (Owner)" 


//var emailBytoken = 

 const emails = ['username@gmail.com', 'user02@gmail.com'];
const styles = {
  avatar: {
 
  },
};

class SimpleDialog extends React.Component {
  handleClose = () => {
    this.props.onClose(this.props.selectedValue);
  };

  handleListItemClick = value => {
    this.props.onClose(value);
  };

  render() {
    const { classes, onClose, selectedValue, ...other } = this.props;

    return (
      <Dialog onClose={this.handleClose}  {...other}>
        <DialogTitle id="simple-dialog-title">Collaborators</DialogTitle>
        <Divider />  
        <div>
          <List>
          <ListItem value={emailOwner} >
                <ListItemAvatar>
                  <Avatar className={classes.avatar}>
                    <PersonIcon /> 
                  </Avatar>
                </ListItemAvatar>
                <ListItemText primary={emailOwner}  />
              </ListItem>
                      
              <div id="addiconWithText">
            <ListItem>                
             <AddCollabarate />
              
            </ListItem>
            </div>
          </List>
        </div>
      </Dialog>
    );
  }
}

SimpleDialog.propTypes = {
  classes: PropTypes.object.isRequired,
  onClose: PropTypes.func,
  selectedValue: PropTypes.string,
};

const SimpleDialogWrapped = withStyles(styles)(SimpleDialog);

export default class Collaborator extends React.Component {
  state = {
    open: true,
    selectedValue: emails[1], 
  };

  // handleClickOpen = () => {
  //   this.setState({
  //     open: true,
  //   });
  // };

  handleClose = value => {
    this.setState({ selectedValue: value, open: false });
  };

  render() {
    return (
      <div>
      
        <br />
        {/* <Button variant="outlined" color="primary" onClick={this.handleClickOpen}>
          Open simple dialog
        </Button> */}
        <SimpleDialogWrapped
          selectedValue={this.state.selectedValue}
          open={this.state.open}
          onClose={this.handleClose}
        />
      
      </div>
    );
  }
}

