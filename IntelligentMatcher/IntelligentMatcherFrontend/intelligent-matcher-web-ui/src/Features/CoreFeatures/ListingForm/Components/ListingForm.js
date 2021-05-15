import React, {createRef, useState} from 'react'
import { ButtonToolbar } from 'react-bootstrap';
import { Form,Input,Select,TextArea,Button,Grid,href,color} from 'semantic-ui-react'
import './ListingForm.css'
import '../.././../../index'



function ListingForm(props) {

    const [TitleState, setTitleState] = useState("");
    const [DetailsState, setDetailsState] = useState("");
    const [CityState, setCityState] = useState("");
    const [StateState, setStateState] = useState("");
    const [InpersonOrRemoteState, setInpersonOrRemoteState] = useState("");
    const [NumberOfParticipantState, setNumberOfParticipantState] = useState("");
    const [UserAccountIdState, setUserAccountIdStateState] = useState("");
   
    function submitHandler(e){
      var ListingModel = e;
      fetch(global.url + 'ListingForm/FilloutForm',
      {
      method: "POST",
      headers: {'Content-type':'application/json'},
      body: JSON.stringify(ListingModel)
      }).
      then(r => r.json()).then(res=>{
          if(res.success){
              alert("Registration Success");
              
          }
          else{
              alert(res.errorMessage);
          }
      }
      );


    }

    return (
        <Form>
        <Form.Field
          width= {3}
          id='form-input-control-title'
          control={Input}
          label='ListingTitle'
          placeholder= 'ListingTitle'
          onChange={e => setTitleState(e.target.value)}
        
        />
        <Form.Field
          id='form-textarea-control-details'
          control={TextArea}
          label='Details'
          placeholder='Details'
          onChange={e => setDetailsState(e.target.value)}
        />

         <Form.Group widths='equal' width={2}>
          <Form.Field
            id='form-input-control-city'
            control={Input}
            label='City'
            placeholder='City'
            onChange={e => setCityState(e.target.value)}
          />
          <Form.Field
            id='form-input-control-state'
            control={Input}
            label='State'
            placeholder='State'
            onChange={e => setStateState(e.target.value)}
          />
          <Form.Field
            id='form-input-control-state'
            control={Input}
            label='InpersonOrRemote'
            placeholder='InpersonOrRemote'
            onChange={e => setInpersonOrRemoteState(e.target.value)}
          />
           <Form.Field
            id='form-input-control-state'
            control={Input}
            label='NumberofParticipants'
            placeholder='NumberofParticipants'
            onChange={e => setNumberOfParticipantState(e.target.value)}
          />
         
        
        </Form.Group>
        <Button href={global.urlRoute + "ListingCategoryPage"} content="Pick your Categories" color="blue"/>
        <Button  onClick={()=>submitHandler({
                    Title:TitleState,
                    Details:DetailsState,
                    City:CityState,
                    State:StateState,
                    InPersonOrRemote:InpersonOrRemoteState,
                    NumberofParticipants:parseInt(NumberOfParticipantState,10),
                    UserAccountId:101,
                    CollaborationType: "string",
                    InvolvementType : "string",
                    Experience: "string"

               
                })}content="Confirm" color="black"/>
        
      </Form>
      
    )

}


export default ListingForm;