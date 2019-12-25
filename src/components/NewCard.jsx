 import React from 'react'
import Card from "@material-ui/core/Card";
import TextareaAutosize from "@material-ui/core/TextareaAutosize";
import EditLocationOutlinedIcon from "@material-ui/icons/EditLocationOutlined";
 export default class NewCard extends React.Component{
render()
{
  return(
       <div id="Small-NotesCardInner">
          <Card id="CardIdAllNotes" onClick={()=>this.operation(item)}  >
            
              <div >
                <div className="Small-NotesTitleAndDesc">
                  <TextareaAutosize
                    id="titleId"
                    name="notesTitle"
                    value={item.notesTitle}
                    placeholder="Title"
                    onClick={()=>this.operation(item.notesTitle, item.notesDescription)}
                  />
                  <EditLocationOutlinedIcon />
                  <TextareaAutosize 
                    id="DescriptionId"
                    name="notesDescription"
                    value={item.notesDescription}
                    placeholder="Description"
                  />
                </div>

                <div className="Small-closeButton">
                  <Icons noteid={item} />
                </div>
              </div>
            
          </Card>
        </div>
      ) 
}


 }
 
 