import React, { Component } from 'react'
import '../css/getAllNotesCSS.css';
import  AxiosService  from '../service/postData';
import { Button, TextField, IconButton } from '@material-ui/core';
import DashBoard from './DashBoard'
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';


var axiosObject = new AxiosService;
export default class EditLabel extends Component {
    constructor(props) {
        super(props);
        this.state = {
            AllLabel: [],
            getAllLabel: [],
            labelValue: '',
            name: this.props.labeldata

        }

        this.getLabelsNotes = this.getLabelsNotes.bind(this);
        this.EditLabelData = this.EditLabelData.bind(this);
        this.DeleteLabelData = this.DeleteLabelData.bind(this);



    }

    componentDidMount() {

        this.getLabelsNotes()
        console.log("this is data label")
    }

    EditLabelData()
    {
        var data = {
                 
            Id: this.props.labelId,                             
            name : this.state.name,

          }

          axiosObject.EditLabelService(data).then(response=>{
              console.log(" response in ",response);
           
             
            })
    }

    DeleteLabelData=() =>
    {
          axiosObject.DeleteLabelService(this.props.labelId).then(response=>{
              console.log(" response in ",response);
            })
    }

    getLabelsNotes() {
        console.log("This is funtion")

        axiosObject.GetLabelService().then(response => {
            console.log(response);
            let array = [];
            console.log(" log ", response);

            response.data.result.map((data) => {
                array.push(data);
            });
            this.setState({
                getAllLabel: array
            })

        });

    }
 
   
    handleChange = name => event => {
        this.setState({ [name]: event.target.value });
    };
    render() {

        console.log("name edit with label ", this.props.labeldata);
        console.log("this is Id of label ", this.props.labelId);
        return (
            <div>
             <IconButton size="small" onClick={this.DeleteLabelData}>
            <DeleteIcon fontSize="inherit" id="DeleteIcon" onClick={this.DeleteLabel} />
           </IconButton>
                <TextField
                    id="standard-name"
                    value={this.state.name}
                    onChange={this.handleChange('name')}
                    margin="normal"
                />
                <IconButton size="small" onClick={this.EditLabelData}>
                    <EditIcon fontSize="inherit" id="DeleteIcon"/>
                </IconButton>
                <div>
                </div>
            </div>
        )

    }
}
