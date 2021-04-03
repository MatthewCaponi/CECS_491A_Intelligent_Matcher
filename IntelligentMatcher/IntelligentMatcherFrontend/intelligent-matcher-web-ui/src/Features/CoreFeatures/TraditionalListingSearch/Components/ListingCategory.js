




function ListingCategory(props){
    const[timedOut, setTimedOut] = useState('False');
    if (props.rows.length === 0) {
        return (
            <Grid container centered>
                <Grid.Row />
                <Grid.Row />
                <Grid.Row>
                    <Dimmer inverted active>
                        <Loader size="massive"/>
                    </Dimmer>
                </Grid.Row>          
            </Grid>    
        )
    }


const listingStyle = {
    display: 'block',
    height: '40vh',
    overflowY: "auto",

  };
}

return(
    <Grid container centered>


        
    </Grid>
)    
