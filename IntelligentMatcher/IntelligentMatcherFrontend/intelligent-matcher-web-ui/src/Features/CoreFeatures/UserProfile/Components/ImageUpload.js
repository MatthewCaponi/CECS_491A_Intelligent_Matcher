import React from 'react';    
import { Table, Grid } from 'semantic-ui-react'
import ReactDataGrid from 'react-data-grid';
import { post } from 'axios';    

class ImageUpload extends React.Component {    

        constructor(props) {    
                super(props);    
                this.state = {    
                    viewingId: 0,
                    file: '',    
            };    

            let url = window.location.href;
            url = url.split("id=")
            this.state.viewingId = parseInt(url[1]);  
        }    

        async submit(e) {    
                e.preventDefault();    
                const url = `http://localhost:5000/UserProfile/UploadPhoto`;    
                const formData = new FormData();    
                formData.append('body', this.state.file);    
                formData.append("userId", this.state.viewingId);
                const config = {    
                       headers: {    
                               'content-type': 'multipart/form-data',    
                        },    
                };    
                return post(url, formData, config);    
        }    

        async setFile(e) {    
                await this.setState({ file: e.target.files[0] });    
                await this.submit(e);
        }    

        render() {    
                return (    
                 <form class="ui form" onSubmit={e => this.submit(e)}>    
                         <input class="ui button" type="file" onChange={e => this.setFile(e)} />    
                 </form>    
             )    
        }    
}    

export default ImageUpload   