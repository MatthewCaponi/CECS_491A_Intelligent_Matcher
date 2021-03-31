import React from 'react';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import { USERS_API_URL } from '../constants';
class RegistrationForm extends React.Component {

    render() {
        return <Form onSubmit={this.submitEdit}>
            <FormGroup>

                <Label for="password">Password:</Label>
                <Input type="password" name="password" onChange={this.onChange} value={this.state.password === '' ? '' : this.state.password} />

                <Label for="email">New Email:</Label>
                <Input type="text" name="email" onChange={this.onChange} value={this.state.email === '' ? '' : this.state.email} />
            </FormGroup>
            <Button>Change Email</Button>
        </Form>;
    }
}
export default RegistrationForm;