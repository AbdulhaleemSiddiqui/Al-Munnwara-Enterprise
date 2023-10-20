import React, { useState } from "react";

import './login.css';

function Login(props) {
    // React States
    const [errorMessages, setErrorMessages] = useState({});
    const [isSubmitted, setIsSubmitted] = useState(false);
    const [uname, setName] = useState("");
    const [pass, setPassword] = useState("");

    const LoginUser = async () => {
        console.log("aa");
        try {
            const result = await fetch('api/LoginUser', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    name: uname,
                    password: pass
                })
            });
            const json = await result.json();
            if (json === "") {
                setIsSubmitted(true);
               this.setState({
                    isSubmitted: true
                })
            }
            else {
                setErrorMessages({
                    name: "pass", // Update the state to indicate the error
                    message: json // Provide an appropriate error message
                });
            }
        } catch (error) {
            setErrorMessages({
                name: "pass", // Update the state to indicate the error
                message: "An error occurred" // Provide an appropriate error message
            });
        }
    }

    const handleChange = (e) => {
        if (e.target.name === "uname") {
            setName(e.target.value); // Update the 'uname' state
        } else if (e.target.name === "pass") {
            setPassword(e.target.value); // Update the 'pass' state
        }
    }

    const handleSubmit = async (event) => {
        event.preventDefault();

        props.onLogin();
        //await LoginUser();
        //console.log(isSubmitted); //// Call LoginUser function
        //if (isSubmitted) {
        //    props.onLogin();
        //}
     
    };

    const renderForm = (
        <div className="form">
            <form onSubmit={handleSubmit}>
                <div className="input-container">
                    <p>{errorMessages.message}</p>
                    <label>Username</label>
                    <input type="text" name="uname" onChange={handleChange} required />
                </div>
                <div className="input-container">
                    <label>Password</label>
                    <input type="password" name="pass" onChange={handleChange} required />
                </div>
           
                <div className="button-container">
                    <input className="custom-button" type="submit" />
                </div>
               
            </form>
        </div>
    );

    return (
        <div className="Login">
            <div className="login-form">
                <div className="title">Sign In</div>
                { renderForm}
            </div>

               
         
        </div>
    );
}

export default Login;
