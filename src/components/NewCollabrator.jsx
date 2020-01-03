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
import Tooltip from '@material-ui/core/Tooltip';
import PersonAddIcon from '@material-ui/icons/PersonAdd';
import IconButton from '@material-ui/core/IconButton';
import CheckIcon from '@material-ui/icons/Check';
import Badge from '@material-ui/core/Badge';
import ClearOutlinedIcon from '@material-ui/icons/ClearOutlined';
import AxiosService from '../service/postData'


var JwtToken = localStorage.getItem('Token')
var decoded = jwt_decode(JwtToken)
var emailOwner = decoded.Email + "      (Owner)"

var axiosObject = new AxiosService()

const emails = ['username@gmail.com', 'user02@gmail.com'];
const styles = {
  avatar: {
    backgroundColor: blue[100],
    color: blue[600],
  },
};

class SimpleDialog extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      text: '',
      open: '',
      list: [],
      id:''

    }
    this.handleSubmit = this.handleSubmit.bind(this);
    this.getCollabarotor = this.getCollabarotor.bind(this);
  }
  handleClose = () => {
    this.props.onClose(this.props.selectedValue);
    this.getCollabarotor();
  };

  handleClickOpen = () => {
    this.setState({
      open: true,
    });
  };

  removeItem(index) {
    const list = this.state.list;
    list.splice(index, 1);
    this.setState({ list });
  }

  handleListItemClick = value => {
    this.props.onClose(value);
  };

  componentWillReceiveProps(newProps) {

  }
  getCollabarotor() {
   this.setState({
     id:this.props.idItem
   })
   console.log("This is funtion Simple dialog ",this.props.idOfData)
        var data = {
               
          list: this.state.list,                             
          Id : this.props.idOfData       
        }

      axiosObject.AddCollabratorService(data).then(response => {
            console.log(" response in ",response);
          })            

  }
  ChangeHandler(e) {
    this.setState({
      text: e.target.value
    });
  }
  handleSubmit(e) {
    e.preventDefault();
    this.setState({
      list: this.state.list.concat(this.state.text),
      text: ""
    });
    
  }
  render() {
    console.log("This is collabarot iddddd ", this.props.idOfData);

    const { classes, onClose, selectedValue, ...other } = this.props;

    return (
      <Dialog onClose={this.handleClose} aria-labelledby="simple-dialog-title" {...other}>
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
              <ListItemText primary={emailOwner} />
            </ListItem>

            <div className="addcolabarateList">
          {this.state.list.map((item, index) => {
            return <li id="listItem" key={index}>
             
                <IconButton size="small" color="black">
                  <Badge color="secondary">
                    < AddCircleIcon fontSize="inherit"  precision={1} />
                  </Badge>
                </IconButton>
                
            
              {item}

              <button onClick={() => this.removeItem(index)}>
                <ClearOutlinedIcon />
              </button>
            </li>;
          })}
        </div>

            <div id="addiconWithText">
              <ListItem>
             
                <Tooltip title="Collaborate" enterDelay={250} leaveDelay={100}>
                  <IconButton id="IconButton" size="small" color="black" onClick={this.handleClickOpen}>
                    <Badge color="secondary">
                      <PersonAddIcon fontSize="inherit" />
                    </Badge>
                  </IconButton>
                </Tooltip>
               
                <TextField id="textfieldEmail"
                  type="text"
                  value={this.state.text}
                  onChange={e => this.ChangeHandler(e)}
                />

                <Button onClick={this.handleSubmit}>
                  <CheckIcon />
                </Button>
              </ListItem>
            </div>
          </List>
        </div>
        <Divider />
        <div className="save-cancel-div">
          <Button>Cancel</Button>
          <Button onClick={this.handleClose}>Save</Button>
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


export default class NewCollabrator extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      open: false,
      text: "",
      list: []
      //// selectedValue: emails[1], 
    };
    this.handleSubmit = this.handleSubmit.bind(this);
    this.ChangeHandler = this.ChangeHandler.bind(this);
    this.removeItem = this.removeItem.bind(this);
  }


  handleClickOpen = () => {
    this.setState({
      open: true,
    });
  };

  handleClose = value => {
    this.setState({ selectedValue: value, open: false });
  };

  handleCloseSaveBtn = () => {
    this.setState({
      open: false,

    });
  };

  removeItem(index) {
    const list = this.state.list;
    list.splice(index, 1);
    this.setState({ list });
  }
  handleSubmit(e) {
    e.preventDefault();
    this.setState({
      list: this.state.list.concat(this.state.text),
      text: ""
    });
  }
  ChangeHandler(e) {
    this.setState({
      text: e.target.value
    });
  }

  render() {
    console.log("this is id of note item ", this.props.idItem);

 const Id=this.props.idItem;
 console.log("this is id const ", Id);
    return (
      <div>

        <Tooltip title="Collaborate" enterDelay={250} leaveDelay={100}>
          <IconButton size="small" color="black" onClick={this.handleClickOpen}>
            <Badge color="secondary">
              <PersonAddIcon fontSize="inherit"/>
            </Badge>
          </IconButton>
        </Tooltip>
        <SimpleDialogWrapped
          selectedValue={this.state.selectedValue}
          open={this.state.open}
          onClose={this.handleClose}
          idOfData={this.props.idItem}
        />

      </div>
    );
  }
}