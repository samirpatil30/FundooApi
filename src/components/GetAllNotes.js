import React, { Component } from 'react'
import Notes from './Notes'
import  {DisplayNotes} from './DisplayNotes'
import '../css/NotesCSS.css'
import { Button } from '@material-ui/core';
import AxiosService from '../service/postData';

var getnotes = new AxiosService;
export default class GetAllNotes extends Component
 {
    constructor(props){
        super(props);
        this.state={
            AllNotes:[],
            getAllNotes:[]
        }

        this. getNotesUser = this. getNotesUser.bind(this);
        this.onchange = this.onchange.bind(this);
    }

    componentDidMount(){
        this.setState({ AllNotes: this.getAllNotes })
       
    }

    getNotesUser() {
        getnotes.GetNotesService().then(response => {
                        console.log(response);
                        let array = [];
                        response.data.result.map((data) => {
                        array.push(data);
                });
            this.setState({
                getAllNotes: array
            })
            console.log('this is state', this.state.getAllNotes);
            
        });
        console.log('state notes array ',this.state.getAllNotes);
    }

  onchange(e)
  {
    this.setState({[e.target.name]: e.target.value});
    console.log(this.state);
  }
render()
    {
        console.log('bvdfshd');
        
    return(
            <div  className="notes-top-create">
                <div>
                    <Notes />
                    <Button onClick={this.getNotesUser}>ClcikMe</Button>
                </div>

                <div>
                    <DisplayNotes notes={this.state.getAllNotes}></DisplayNotes>             
                </div>
            </div>

            )
    }
}