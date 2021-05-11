import React from 'react';
import {  useState } from 'react';

export const AuthnContext = React.createContext({
    isAuthenticated: false,
    login: () => {}
});

const AuthnContextProvider = props => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [role, setRole] = useState("");

    const loginHandler = (r) => {
        setIsLoggedIn(true);
        console.log("rooeee" + r);
        setRole(r);
    }

    const logoutHandler = () => {
        setIsLoggedIn(false);
    }

    const roleHandler = () => {
        setRole("Hello");
    }
    return (
        <AuthnContext.Provider value={{login: loginHandler, logout: logoutHandler, changeRole: roleHandler, isLoggedIn: isLoggedIn, getRole: role}}>
            {props.children}
        </AuthnContext.Provider>
    )
}

export default AuthnContextProvider;