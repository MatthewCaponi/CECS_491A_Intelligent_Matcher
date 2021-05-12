import React, {useState, useEffect} from 'react';
import { Grid, Button } from 'semantic-ui-react'
import '../.././../../index'

import './Archive.css';

function Archive(){

    const [startDateState, setStartDateState] = useState("");
    const [endDateState, setEndDateState] = useState("");
    const [categoryState, setCategoryState] = useState("");
    const [listState, setListState] = useState([]);

    useEffect( () => {
        fetch(global.url + 'Archive/GetCategories')
        .then(response => response.json())
        .then(responseData => {
            setListState(responseData);
        });
    }, [])

        let categoryList = listState.length > 0 && 
        listState.map((category, i) =>
        {
            return(
                <option key={i} value={category}>{category}</option>
            )
        })
    

    function archiveAllHandler(e){
        var ArchiveModel = e;
        if(e.startDate != "" && e.endDate != ""){
            fetch(global.url + 'Archive/ArchiveLogFiles',
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

    function archiveCategoryHandler(e){
        var ArchiveModel = e;
        if(e.startDate != "" && e.endDate != "" && e.category != "" && e.category != "none"){
            fetch(global.url + 'Archive/ArchiveLogFilesByCategory',
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

    function recoverAllHandler(e){
        var ArchiveModel = e;
        if(e.startDate != "" && e.endDate != ""){
            fetch(global.url + 'Archive/RecoverLogFiles',
            {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(ArchiveModel)
            }).
            then(r => r.json()).then(res=>{
                if(res.success){
                    alert("Recovery Complete");
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

    function deleteAllHandler(e){
        var ArchiveModel = e;
        if(e.startDate != "" && e.endDate != ""){
            fetch(global.url + 'Archive/DeleteArchivedFiles',
            {
            method: "DELETE",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(ArchiveModel)
            }).
            then(r => r.json()).then(res=>{
                if(res.success){
                    alert("Deletion Complete");
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
                    Start Date By Creation Date of the Log (Creation Date Is Different when the log is recovered):
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
                    End Date By Creation Date of the Log (Creation Date Is Different when the log is recovered):
                </label>
            </Grid.Row>
            <Grid.Row>
                <div class="ui input">
                    <input type="date" name="endDate" placeholder="MM/DD/YYYY" onChange=
                    {e => setEndDateState(e.target.value)}/>
                </div>
            </Grid.Row>
            <Grid.Row>
                <label htmlFor="category">
                    Enter a Category of Logs to Archive:
                </label>
            </Grid.Row>
            
            <Grid.Row>
                <div>
                    <select onChange={e => setCategoryState(e.target.value)}>
                        <option key = "0" value="none">Select a category</option>
                        {categoryList}
                    </select>
                </div>
            </Grid.Row>
            <Grid.Row>
                <Button
                    onClick={()=>archiveCategoryHandler({
                        startDate:startDateState,
                        endDate:endDateState,
                        category:categoryState
                    })}
                    compact size="tiny"
                    circular inverted color="green"
                >
                Archived Logs within the Category and Date Range
                </Button>
            </Grid.Row>
            <Grid.Row>
                <Button
                    onClick={()=>archiveAllHandler({
                        startDate:startDateState,
                        endDate:endDateState,
                    })}
                    compact size="tiny"
                    circular inverted color="blue"
                >
                Archive All Logs in Date Range
                </Button>
                <Button
                    onClick={()=>recoverAllHandler({
                        startDate:startDateState,
                        endDate:endDateState,
                    })}
                    compact size="tiny"
                    circular inverted color="blue"
                >
                Recover All Archived Logs in Date Range
                </Button>
            </Grid.Row>
            <Grid.Row>
                <strong>* Once you delete the archives, there's no way to recover them.</strong>
            </Grid.Row>
            <Grid.Row>
                <Button
                    onClick={()=>deleteAllHandler({
                        startDate:startDateState,
                        endDate:endDateState,
                    })}
                    compact size="tiny"
                    circular inverted color="red"
                >
                Delete All Archived Logs in Date Range
                </Button>
            </Grid.Row>
            <Grid.Row>
                <Button href={global.urlRoute} compact size="tiny" circular inverted color="purple">Return Home</Button>
            </Grid.Row>
            </Grid>
        </div>
    )

}

export default Archive;