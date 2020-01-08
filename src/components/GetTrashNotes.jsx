import React, { Component } from 'react'
import DisplayNotes from './DisplayNotes'
import { Button } from '@material-ui/core';
import AxiosService from '../service/postData';
import DashBoard from './DashBoard'

var axiosObject = new AxiosService;
export default class GetTrashNotes extends Component
 {
    constructor(props){
        super(props);
        this.state={
            AllNotes:[],
            TrashNotes:[]

        }

        this. getTrashNotes = this. getTrashNotes.bind(this);
      
    }

    getTrashNotes() {
        axiosObject.GetAllTrashNotesService().then(response => {
                        console.log(response);
                        let array = [];
                        response.data.result.map((data) => {
                        array.push(data);
                });
            this.setState({
                TrashNotes: array
            })
            
        });
        console.log('state notes array ',this.state.getAllNotes);
    }

    componentDidMount(){
        this.setState({ AllNotes: this.TrashNotes })
       this.getTrashNotes();
    }


render()
    {
        
    return(
            <div  className={this.state.open?null:"notes-top-create"}>

                <div>
                    <DisplayNotes notes={this.state.TrashNotes} getAllTrashNotes={ this.getTrashNotes}></DisplayNotes>    
                    {/* <DashBoard notesInDashBoard={this.state.getAllNotes} />          */}
                </div>
            </div>

            )
    }
}