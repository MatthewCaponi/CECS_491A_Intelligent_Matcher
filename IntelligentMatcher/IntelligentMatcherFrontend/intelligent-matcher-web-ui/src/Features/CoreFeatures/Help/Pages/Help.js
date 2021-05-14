import React, {useState, useEffect} from 'react';
import { Header, Segment, Icon, Menu, Input, Container, Grid, Label, Dropdown, Button} from 'semantic-ui-react';
import './Help.css';

function Help() {
    const [subjectState, setSubjectState] = useState("");
    const [messageState, setMessageState] = useState("");

    function submitHandler(e){
        var HelpModel = e;
        // e.preventDefault();
        if(e.subject != "" && e.message != "" ){
            fetch(global.url + 'Help/SendHelpEmail',
            {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(HelpModel)
            }).
            then(r => r.json()).then(res=>{
                if(res){
                    alert("Email sent successfully!");
                }
                else{
                    alert("Email was not sent!");
                }
            }
            );
        }
        else{
            alert("Input is Empty");
        }
    }

    return(
        <div>
            <Grid container>
            <Grid.Row>
                <h1>Need Help? Contact Us!</h1>
            </Grid.Row>
            <Grid.Row>
                <label htmlFor="subject">
                    Subject:
                </label>
            </Grid.Row>
            <Grid.Row verticalAlign="middle">
                <div class="ui input">
                    <input type="text" name="subject" placeholder="Subject" onChange={e => setSubjectState(e.target.value)}/>
                </div>
            </Grid.Row>
            <Grid.Row>
                <label htmlFor="message">
                    Message:
                </label>
            </Grid.Row>
            <Grid.Row verticalAlign="middle">
                <div class="ui input">
                    <textarea
                        value={messageState}
                        placeholder="Enter Message"
                        onChange={e => setMessageState(e.target.value)}
                        style={{minHeight: 100, minWidth: 300}}
                    />
                </div>
            </Grid.Row>
            <Grid.Row>
                <Button
                    onClick={()=>submitHandler({
                        subject:subjectState,
                        message:messageState
                    })}
                    compact size="tiny"
                    circular inverted color="red"
                >
                Submit
                </Button>
            </Grid.Row>
            </Grid>
        </div>
    )
}

export default Help;