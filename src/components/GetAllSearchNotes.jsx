import React, { Component } from 'react'
import { Button } from '@material-ui/core';
import AxiosService from '../service/postData';

var axiosObject = new AxiosService;
export default class GetAllSearchNotes extends Component
 {
    constructor(props){
        super(props);
        this.state={
            AllNotes:[],
            SearchNotes:[],


        }
        this. getSearchNotes = this. getSearchNotes.bind(this);  
    }


    // componentDidUpdate(){
    //     // this.setState({ AllNotes: this.SearchNotes })
    //    this.getSearchNotes();
    // }
    


render()
    {
        console.log('this is serarch word', this.state.searchWord);
        
    return(
            <div >

                <div>
                     
                    <DashBoard notesInDashBoard={this.state.getAllNotes} />         
                </div>
            </div>

            )
    }
}