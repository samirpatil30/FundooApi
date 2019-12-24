import React, { Component } from "react";
import ReactDOM from "react-dom";
import Button from '@material-ui/core/Button'
import AddCircleIcon from '@material-ui/icons/AddCircle';
import IconButton from '@material-ui/core/IconButton';
import Badge from '@material-ui/core/Badge';
import Avatar from '@material-ui/core/Avatar';
import '../css/CollabratorCSS.css';
import { TextField } from "@material-ui/core";
import CheckIcon from '@material-ui/icons/Check';
import ClearOutlinedIcon from '@material-ui/icons/ClearOutlined';


export default class AddCollabarate extends Component {
  constructor(props) {
    super(props);
    this.state = {
      text: "",
      list: []
    };
    this.handleSubmit = this.handleSubmit.bind(this);
    this.ChangeHandler = this.ChangeHandler.bind(this);
    this.removeItem = this.removeItem.bind(this);
  }

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
    return (
      <div>
    
        <div className="addcolabarateList">
          {this.state.list.map((item, index) => {
            return <li id="listItem" key={index}>  
               <Avatar >
                <IconButton  color="black"> 
                             <Badge  color="secondary">
                             < AddCircleIcon id="IconButtonAdd" precision={1} />
                             </Badge>
                             </IconButton>               
                  </Avatar>
                  {item}
                  <button onClick={() => this.removeItem(index)}>
                  <ClearOutlinedIcon />

                  </button>
             </li>;
          })}
        </div>

        <div className="button-icon-textfielddiv">
               <Avatar >
                <IconButton  color="black"> 
                             <Badge  color="secondary">
                             < AddCircleIcon id="IconButtonAdd" precision={1} />
                             </Badge>
                             </IconButton>               
                  </Avatar>
        <TextField id="textfieldEmail"
          type="text"
          value={this.state.text}
          onChange={e => this.ChangeHandler(e)}
        />
        <Button onClick={this.handleSubmit}>
            <CheckIcon />
        </Button>
        </div>
      </div>
    );
  }
}



