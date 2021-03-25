import React from 'react';
import Navbar from 'react-bootstrap/Navbar'
import Container from 'react-bootstrap/Container'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMoon } from '@fortawesome/free-solid-svg-icons';
import './SiteHeader.css'


function SiteHeader() {
    return (
        <Navbar bg="dark" variant="dark" fixed="top" expand="lg">
            <FontAwesomeIcon icon={faMoon} className="component-moon" />
            <Navbar.Brand>InfiniMuse</Navbar.Brand>
        </Navbar>
    )
}
export default SiteHeader;