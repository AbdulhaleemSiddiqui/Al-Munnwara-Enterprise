import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Quotation } from './components/Quotation/Quotation';
import { PurchaseOrder } from './components/PurchaseOrder/PurchaseOrder';
import Purchasing  from './components/Purchasing/Purchasing.js';
import { Counter } from './components/Counter';
import './custom.css'
import { User } from './components/Add/User/User';
import { Item } from './components/Add/Item/Item';
import { Unit } from './components/Add/Unit/Unit';
import { Tax } from './components/Add/Tax/Tax';
import { Vendor } from './components/Add/Vendor/Vendor';
import Login from './components/Login';

export default class App extends Component {
    static displayName = App.name;
    constructor(props) {
        super(props);
        this.state = {
            isAuthenticated: false,
        };
    }
    handleLogin = () => {
        this.setState({ isAuthenticated: true });
    }
    render() {
        const { isAuthenticated } = this.state;

        return (

            <div>
                {isAuthenticated ? (
                    <Layout>
                   
                        <Route exact path="/" component={Home} />
                        <Route path="/counter" component={Counter} />
                        <Route path="/Quotation" component={Quotation} />
                        <Route path="/Purchasing" component={Purchasing} />
                        <Route path="/PurchaseOrder" component={PurchaseOrder} />
                        <Route path="/User" component={User} />
                        <Route path="/Item" component={Item} />
                        <Route path="/Unit" component={Unit} />
                        <Route path="/Tax" component={Tax} />
                        <Route path="/Vendor" component={Vendor} />
                    </Layout>
                ) : (
                    <Login onLogin={this.handleLogin} />
                )}
            </div>
        );
    }
}
