import React, {useState, useEffect} from 'react';
import ListingCategories from '../Components/ListingCategories';
import { Grid, Header, Divider, Label, Search,Button,Message, Container, Visibility,color } from 'semantic-ui-react'
import { Router } from 'react-router';


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
        <Grid container centered>
            <Grid row />
            <Grid rows />
            <Message color= "violet" size="large">Please Select your main Category</Message>
            <Divider hidden></Divider>
            <Grid row />   
            <Grid container columns centered >  
                <Button content="VideoGameTeam" primary/>
                <Button id="helpButton" content="Personal Help" primary/>
                <Button content="Hobbies" primary/>
                <Button content="Dating" primary/>
                <Button content="Goals" primary/>
                <Button content="Ideas" primary/>
                <Button content="Projects" primary/>
                <Button content="Friends" primary/>
            </Grid>
            <Divider hidden/>
            <Grid rows></Grid>
            <Grid container columns left>
                <Message id="gamegenre" color="blue" content="Game genre" />
            </Grid>
            <Divider hidden>
                <Grid row></Grid>
                <Grid container centered columns rows>
                <Button inverted content="Action" secondary/>
                <Button inverted content="Sports" secondary/>
                <Button inverted content="Adventure" secondary/>
                <Button inverted content="Battle Royale" secondary/>
                <Button inverted content="Role-Playing" secondary/>
                <Button inverted content="Racing" secondary/>
                <Button inverted content="Party Games" secondary/>
                </Grid>
                <Divider hidden/>
                <Divider hidden/>
                <Grid container centered columns>
                <Button inverted content="Fighting" secondary/>
                <Button inverted content="Real-Time Strategy" secondary/>
                <Button inverted content="First Person Shooter" secondary/>
                <Button inverted content="Third Person" secondary/>
                <Button inverted content="Puzzle" secondary/>
                <Button inverted content="Retro/Arcade" secondary/>
                </Grid>
                
            </Divider>
            <Divider hidden/>
            <Grid rows></Grid>
            <Grid container columns >
                <Divider hidden></Divider>
            </Grid>
            <Divider hidden></Divider>
            <Divider hidden>
                <Grid container leftcolumns row>
                <Button inverted color="orange"size="medium" content="Single Player" />
                <Button  inverted color="orange"size="medium" content="MultiPlayer" />
                </Grid>
               
               
            </Divider>
        
            <Grid rows></Grid>
            <Grid container columns >
                <Divider hidden></Divider>
            </Grid>
            <Divider hidden></Divider>
            <Divider hidden>
                <Grid container left columns row>
                <Button inverted color="orange"size="medium" content="Competitive" />
                <Button  inverted color="orange"size="medium" content="Co-op" />
                </Grid>
               
               
            </Divider>
            
            
            
        </Grid>
        


        
    )
}



export default ListingCategoryPage;