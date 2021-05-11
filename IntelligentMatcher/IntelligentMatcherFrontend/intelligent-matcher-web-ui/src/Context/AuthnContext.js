import React from 'react';
import {  useState } from 'react';

export const AuthnContext = React.createContext({
    isAuthenticated: false,
    login: () => {}
});

const AuthnContextProvider = props => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    const loginHandler = () => {
        setIsLoggedIn(true);
    }

    const logoutHandler = () => {
        setIsLoggedIn(false);
    }
    return (
        <AuthnContext.Provider value={{login: loginHandler, logout: logoutHandler, isLoggedIn: isLoggedIn}}>
            {props.children}
        </AuthnContext.Provider>
    )
}

export default AuthnContextProvider;