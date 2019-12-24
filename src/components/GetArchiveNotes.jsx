import React, { Component } from "react";
import DisplayNotes from "./DisplayNotes";
import { Button } from "@material-ui/core";
import AxiosService from "../service/postData";
import DashBoard from "./DashBoard";

var axiosObject = new AxiosService();
export default class GetArchiveNotes extends Component {
  constructor(props) {
    super(props);
    this.state = {
      AllNotes: [],
      ArchiveNotes: []
    };

    this.getArchiveNotes = this.getArchiveNotes.bind(this);
    this.onchange = this.onchange.bind(this);
  }

  getArchiveNotes() {
    axiosObject.GetAllArchiveNotesService().then(response => {
      console.log(response);
      let array = [];
      response.data.result.map(data => {
        array.push(data);
      });
      this.setState({
        ArchiveNotes: array
      });
    });
    console.log("state notes array ", this.state.getAllNotes);
  }

  componentDidMount() {
    this.setState({ AllNotes: this.ArchiveNotes });
    this.getArchiveNotes();
  }
  onchange(e) {
    this.setState({ [e.target.name]: e.target.value });
    console.log(this.state);
  }
  render() {
    return (
      <div className={this.state.open ? null : "notes-top-create"}>
        {/* <div>
                    <Notes />          
                </div> */}

        <div>
          <DisplayNotes notes={this.state.ArchiveNotes}></DisplayNotes>
          {/* <DashBoard notesInDashBoard={this.state.getAllNotes} />          */}
        </div>
      </div>
    );
  }
}
