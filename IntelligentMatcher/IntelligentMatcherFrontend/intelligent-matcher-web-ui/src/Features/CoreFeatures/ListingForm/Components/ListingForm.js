import React, {createRef, useState} from 'react'
import { ButtonToolbar } from 'react-bootstrap';
import { Form,Input,Select,TextArea,Button,Grid,href,color} from 'semantic-ui-react'
import './ListingForm.css'
import '../.././../../index'



function ListingForm(props) {
    return (
        <Form>
        <Form.Group widths='equal'>
          <Form.Field
            id='form-input-control-first-name'
            control={Input}
            label='First name'
            placeholder='First name'
          />
          <Form.Field
            id='form-input-control-surname'
            control={Input}
            label='Surname'
            placeholder='Surname'
          />
          
        </Form.Group>
        <Form.Field
          width= {3}
          id='form-input-control-title'
          control={Input}
          label='ListingTitle'
          placeholder= 'ListingTitle'
        
        />
        <Form.Field
          id='form-textarea-control-details'
          control={TextArea}
          label='Details'
          placeholder='Details'
        />

         <Form.Group widths='equal' width={2}>
          <Form.Field
            id='form-input-control-city'
            control={Input}
            label='City'
            placeholder='City'
          />
          <Form.Field
            id='form-input-control-state'
            control={Input}
            label='State'
            placeholder='State'
          />
        
        </Form.Group>
        <Button href={global.urlRoute + "ListingCategoryPage"} content="Pick your Categories" color="blue"/>
        <Button href={global.urlRoute + "ListingTable"} content="Confirm" color="black"/>
        
      </Form>
      
    )

}


export default ListingForm;