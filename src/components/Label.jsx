import React, { Component } from 'react'
import '../css/getAllNotesCSS.css';
import  AxiosService  from '../service/postData';
import { Button, TextField } from '@material-ui/core';
import DashBoard from './DashBoard'
import EditLabel from './EditLabel'

var axiosObject = new AxiosService;
export default class Labeldata extends Component {
    constructor(props) {
        super(props);
        this.state = {
            AllLabel: [],
            getAllLabel: [],
            labelValue: '',
            name: ''
        }
        this.getLabelsNotes = this.getLabelsNotes.bind(this);
    }

    componentDidMount() {

        this.getLabelsNotes()
        console.log("this is data label")
    }
    getLabelsNotes() {
        console.log("This is funtion")

        axiosObject.GetLabelService().then(response => {
            console.log(response);
            let array = [];
            console.log(" log in label", response.data.result);

            response.data.result.map((data) => {
                array.push(data);

                console.log('Arrayyyyyy',array);
                
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

        var printLabelList = this.state.getAllLabel.map((item, index) => {
            const datalabel = item.label
            console.log('data label value ', datalabel);
            return (
                <div>

                    {
                        this.props.editLabelbool === false ? <Button id="span-id-label">{item.label}  </Button> : 
                        <EditLabel labeldata={item.label} labelId={item.LabelId}/>
                    }

                    <div>

                    </div>
                </div>

            )
        })

        return (
            <div>
                {printLabelList}
            </div>
        )
    }
}