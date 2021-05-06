import React, {useState, useEffect} from 'react';
import { Grid, Button } from 'semantic-ui-react'

import './Archive.css';

function Archive(){

    const [startDateState, setStartDateState] = useState("");
    const [endDateState, setEndDateState] = useState("");

    function archiveAllHandler(e){
        var ArchiveModel = e;
        if(e.startDate != "" && e.endDate != "" ){
            fetch('http://localhost:5000/Archive/ArchiveLogFiles',
            {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(ArchiveModel)
            }).
            then(r => r.json()).then(res=>{
                if(res.success){
                    alert("Archive Complete");
                }
                else{
                    alert(res.errorMessage);
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
                <h1>Archive</h1>
            </Grid.Row>
            <Grid.Row>
                <label htmlFor="startDate">
                    Start Date:
                </label>
            </Grid.Row>
            <Grid.Row>
                <div class="ui input">
                    <input type="date" name="startDate" placeholder="MM/DD/YYYY" onChange=
                    {e => setStartDateState(e.target.value)}/>
                </div>
            </Grid.Row>
            <Grid.Row>
                <label htmlFor="endDate">
                    End Date:
                </label>
            </Grid.Row>
            <Grid.Row>
                <div class="ui input">
                    <input type="date" name="endDate" placeholder="MM/DD/YYYY" onChange=
                    {e => setEndDateState(e.target.value)}/>
                </div>
            </Grid.Row>
            <Grid.Row>
                <Button
                    onClick={()=>archiveAllHandler({
                        startDate:startDateState,
                        endDate:endDateState,
                    })}
                    compact size="tiny"
                    circular inverted color="red"
                >
                Archive All Logs
                </Button>
            </Grid.Row>
            <Grid.Row>
                <Button href="http://localhost:3000/" compact size="tiny" circular inverted color="blue">Return Home</Button>
            </Grid.Row>
            </Grid>
        </div>
    )

}

export default Archive;