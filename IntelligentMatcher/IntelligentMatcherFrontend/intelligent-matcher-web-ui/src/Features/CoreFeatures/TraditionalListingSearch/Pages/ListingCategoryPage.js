import React, {useState, useEffect} from 'react';
import ListingCategories from '../Components/ListingCategories';
import { Grid, Header, Divider, Label, Search,Button,Message, Container } from 'semantic-ui-react'


function ListingCategoryPage () {
    const [listings, setListings] = useState([]);
    useEffect( () => {
        fetch('http://localhost:5000/ListingCategory/GetAllCategories')
        .then(response => response.json())
        .then(responseData => {
            setListings(responseData);
            console.log(responseData);
        });
    }, [])



    return (
        <Grid container left>
            <Grid row />
            <Grid row />
            <Message color= "black" size="medium">Please Select your main Category</Message>
            <div></div>
            <Grid row />   
            <Grid row>  
                <Button onClick={()=> alert('Counter reached threshhold, will send a message to the moderators')} content="VideoGameTeam" primary/>
                <Button content="Personal Help" primary/>
                <Button content="Hobbies" primary/>
                <Button content="Dating" primary/>
                <Button content="Goals" primary/>
                <Button content="Ideas" primary/>
                <Button content="Projects" primary/>
                <Button content="Friends" primary/>
            </Grid>
            <Grid column />
            <Grid row>
                <Button content="Game genre" secondary/>

            </Grid>

        </Grid>
        



    )
}



export default ListingCategoryPage;