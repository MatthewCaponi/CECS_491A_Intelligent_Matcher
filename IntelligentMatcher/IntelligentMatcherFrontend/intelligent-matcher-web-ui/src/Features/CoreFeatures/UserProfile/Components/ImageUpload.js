import React from 'react';    
import { Table, Grid } from 'semantic-ui-react'
import ReactDataGrid from 'react-data-grid';
import { post } from 'axios';    
import '../.././../../index'
import Cookies from 'js-cookie';
import jwt from 'jwt-decode';
class ImageUpload extends React.Component {    

        constructor(props) {  
                const idToken = Cookies.get('IdToken');
                const decodedIdToken = jwt(idToken);
                const userId = decodedIdToken.id;  
                super(props);    
                this.state = {    
                    viewingId: 0,
                    file: '', 
                    error: ""   
            };    

            let url = window.location.href;
            url = url.split("id=")
            this.state.viewingId = parseInt(userId);  
        }    

        async submit(e) {                   
                try{           
                        e.preventDefault();    
                        const url = global.url + `UserProfile/UploadPhoto`;    
                        const formData = new FormData();    
                        formData.append('body', this.state.file);    
                        formData.append("userId", this.state.viewingId);
                        const config = {    
                        headers: {    
                                'content-type': 'multipart/form-data',
                                'Authorization': 'Bearer ' + Cookies.get('AccessToken'),    
                                },    
                        };    
                        return post(url, formData, config); 
                }
                catch
                {
                        this.state.error = "Could Not Upload Image"; 
                }

        }    

        async setFile(e) {    
                const files = e.target.files[0].name.split(".");
                if(files[files.length - 1].toLowerCase() == "png" || files[files.length - 1].toLowerCase() == "jpeg" || files[files.length - 1].toLowerCase() == "jpg"){
                        await this.setState({ file: e.target.files[0] });    
                        await this.submit(e);
                }
                else
                {
                        await this.setState({ error: e.target.files[0].name + " is not a compatible file type" });    
                }
        }    
        render() {    
                return (    
                        <div>
                                Upload A New Profile Picture(JPG, JPEG or PNG only)
                                <br />
                                <form class="ui input" onSubmit={e => this.submit(e)}>    
                                        <input class="ui input" type="file" onChange={e => this.setFile(e)} />   
                        
                                </form>         
                                <br /> 
                                {this.state.error}  
                        </div>  
             )    
        }    
}    

export default ImageUpload   