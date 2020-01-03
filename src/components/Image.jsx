import React from 'react';
import ImageUploader from 'react-images-upload';
 import Badge from '@material-ui/core/Badge';
import Tooltip from '@material-ui/core/Tooltip';
import { IconButton } from "@material-ui/core";
import ImageOutlinedIcon from '@material-ui/icons/ImageOutlined';


export default class Image extends React.Component {
 
    constructor(props) {
        super(props);
         this.state = { 
             pictures: [] ,
             showDialog:false,
             };
         this.onDrop = this.onDrop.bind(this);
    }
 
    onDrop=(picture)=> {
        this.setState({
            pictures: this.state.pictures.concat(picture),
        });
    }

    openDialog=()=>
    {
        this.setState({
            showDialog: true
        })
    }
 
    render() {
        console.log('Image', this.state.pictures);
        
        return (
             <div >   
               
                <Tooltip title="Color" enterDelay={250} leaveDelay={100}>
                <IconButton size="small" color="black" onClick={()=> this.ImageOpen.Click()}  >
                <Badge color="secondary">
                <ImageOutlinedIcon fontSize="inherit" />
                </Badge>
                </IconButton>
                </Tooltip>
            
                <div>
                <input style={{display: none}}                 
                        type="file"
                        ref= { ImageOpen=>
                            {
                                ImageOpen = this.ImageOpen    
                            }
                        } />
                </div>
              
          </div>
            
        );
    }
}