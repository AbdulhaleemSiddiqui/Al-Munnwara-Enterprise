import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, Nav, Dropdown, DropdownToggle, DropdownMenu, DropdownItem} from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSignOut } from '@fortawesome/free-solid-svg-icons'

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
            dropdownOpen: false,
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }
    toggleDropdown = () => {
        this.setState({
            dropdownOpen: !this.state.dropdownOpen,
        });
    };
    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" dark>
                    <Container>
                        <NavbarBrand style={{ color: 'black' }} tag={Link} to="/">
                            Al_Munnwar_Enterprises
                        </NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <Nav className="navbar-nav flex-grow">
      
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/Quotation">
                                        Quotation
                                    </NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/PurchaseOrder">
                                        Purchase Order
                                    </NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/Purchasing">
                                        Purchasing
                                    </NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/fetch-data">
                                        Delivery
                                    </NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/fetch-data">
                                        Invoice
                                    </NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/fetch-data">
                                        Reminder
                                    </NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/fetch-data">
                                        Cheque
                                    </NavLink>
                                </NavItem>
               
                                <hr />
                        
                                <NavItem>
                                    <Dropdown isOpen={this.state.dropdownOpen} toggle={this.toggleDropdown}>
                                        <DropdownToggle caret >
                                            Add
                                        </DropdownToggle>
                                        <DropdownMenu>
                                            <DropdownItem tag={Link} to="/Item">
                                                Item
                                            </DropdownItem>
                                            <DropdownItem tag={Link} to="/Vendor">
                                                Vendor
                                            </DropdownItem>
                                            <DropdownItem tag={Link} to="/Tax">
                                                Tax
                                            </DropdownItem>
                                 
                                            <DropdownItem tag={Link} to="/User">
                                               User
                                            </DropdownItem>
                                           
                                            <DropdownItem tag={Link} to="/Unit">
                                                 Unit
                                            </DropdownItem>
                                        </DropdownMenu>
                                    </Dropdown>
                                </NavItem>
                                <hr />
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/Login">

                                        <button type="button" className="btn btn-default btn-sm glyphicon glyphicon-log-out" title="Logout" ><FontAwesomeIcon icon={faSignOut} /> </button>

                                    </NavLink>
                                </NavItem>

                            </Nav>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }

}
