import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFilter } from '@fortawesome/free-solid-svg-icons'

export class PurchaseOrder extends Component {
    constructor(props) {
        super(props);
        this.state = {
            errorMessage: '',
            shipperList: [],
            countryList: [],
            trackingList: [],
            Message: '',
            isError: false,
            NoRecordStyle: '',
            NoRecordError: '',
            NoRecordErrorStyle: '',
            shipperId: '',
            trackingNbr: '',
            originCountry: '',
            weight: 0,
            length: 0,
            height: 0,
            width: 0,
            dimUnit: '',
            weightUnit: '',
            isSuccess: false,
            getDb: 'gp_db_dev',
            saveDb: 'dp_db_dev'
        };
    }


    async componentDidMount() {
        await this.getRate();
        window.scrollTo({ top: 0, behavior: "smooth" });
    }

    async getRate() {

        try {
            const result = await fetch('api/GetAdjustmentRate', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },

            })
            const json = await result.json();
            if (result.ok) {
                if (json.shipper.length) {
                    this.setState({
                        shipperList: json.shipper,
                        countryList: json.country,
                        isError: false,
                        NoRecordError: '',
                        NoRecordStyle: '',
                        NoRecordErrorStyle: ''
                    })

                }
                else {
                    this.setState({
                        NoRecordError: 'No Records Found',
                        NoRecordStyle: 'none',
                        NoRecordErrorStyle: '500px',
                        shipperList: [], countryList: [],
                        totalRecords: json.rowCount,
                    })

                }
            }
            else if (result.status == 401) { 
                this.setState({
                    Message: json,
                    isError: true
                })
            }
            else {
                this.setState({
                    Message: result.statusText || "Unable to get tracking Nbr",
                    isError: true
                })
            }
        }
        catch (error) {
        }
    }
    async getTrackingNbr() {
        try {
            const response = await fetch(`api/GetTrackingNbr?shipperKey=${this.state.shipperId}&originCountry=${this.state.originCountry}&getDb=${this.state.getDb}`, {
                method: 'Get',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                }
            })
            const tNbr = await response.json();


            if (tNbr !== null) {
                this.setState({
                    trackingList: tNbr,
                    isError: false,
                    NoRecordError: '',
                    NoRecordStyle: '',
                    NoRecordErrorStyle: ''
                })

            }
            else {
                this.setState({
                    NoRecordError: 'No Records Found',
                    NoRecordStyle: 'none',
                    NoRecordErrorStyle: '500px',
                    trackingList: [],
                    totalRecords: json.rowCount,
                })

            }
        }



        catch (error) {
        }
    }
    SaveRate = async () => {
        try {
            const result = await fetch("api/SaveRate", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    weight: this.state.weight,
                    width: this.state.width,
                    length: this.state.length,
                    height: this.state.height,
                    dimUnit: this.state.dimUnit,
                    weightUnit: this.state.weightUnit,
                    trackingNbr: this.state.trackingNbr,
                    saveDb: this.state.saveDb
                })
            })
            const response = await result.json();
            if (response.message === "OK") {
                this.setState({
                    isError: false,
                    Message: "Adjustment Rate Record Saved",
                    isSuccess: true,
                    errorMessage: '',

                });
            }
            else if (response === null) {
                this.setState({
                    isError: true,
                    errorMessage: "Adjustment Rate Record Not Saved",
                    Message: '',
                    isSuccess: false,
                });
            }
            this.scrollToTop();
        }
        catch (e) {

        }

    }


    scrollToTop() {
        var scroll = Scroll.animateScroll;
        scroll.scrollToTop();
    }
    handleFiltration = () => {
        if (this.state.originCountry === "Select") {
            this.setState({ originCountry: '' });
        }

        this.getTrackingNbr();
    }


    handleChange = (e) => {
        this.setState({
            [e.target.name]: e.target.value
        });
    }
    handletrackingNbrChange = (e) => {
        this.setState({
            trackingNbr: e.target.value
        });
    }

    render() {

        return (
            <>
                <div className="container-fluid">
                    <br />
                    <div className="card" >

                        <h5 className="card-header">Adjustment Rate</h5>
                        <div className="card-body">
                            <div className={this.state.isError ? "alert alert-danger" : ""} >
                                {this.state.errorMessage}
                            </div>
                            <div className={this.state.isSuccess ? "alert alert-success" : ""} >
                                {this.state.Message}
                            </div>
                            <div style={{ marginTop: "10px" }} className="row">
                                <div className="col-1"></div>
                                <label className="col-2 col-form-label">Get and save DB</label>
                                <div className="col-2">
                                    <select value={this.state.getDb} className="form-control" name="getDb" onChange={this.handleChange} >
                                        <option value='gp_db_dev'>GP DB DEV</option>
                                        <option value='gp_db_qa'>GP DB QA</option>
                                    </select>
                                </div>
                                <div className="col-2">
                                    <select value={this.state.saveDb} className="form-control" name="saveDb" onChange={this.handleChange} >
                                        <option value='dp_db_dev'>DP DB DEV</option>
                                        <option value='dp_db_qa'>DP DB QA</option>
                                    </select>
                                </div>
                            </div>
                            <div style={{ marginTop: "10px" }} className="row">
                                <div className="col-1"></div>
                                <label className="col-2 col-form-label">Shipper Key</label>
                                <div className="col-5">
                                    <select value={this.state.shipperId} className="form-control" name="shipperId" onChange={this.handleChange} >
                                        <option>Select</option>
                                        {this.state.shipperList.map((shipperId) => <option value={shipperId}>{shipperId}</option>)}
                                    </select>
                                </div>
                            </div>
                            <div style={{ marginTop: "10px" }} className="row">
                                <div className="col-1"></div>
                                <label className="col-2 col-form-label">Origin Country</label>
                                <div className="col-5">
                                    <select value={this.state.originCountry} className="form-control" name="originCountry" onChange={this.handleChange} >
                                        <option>Select</option>
                                        {this.state.countryList.map((originCountry) => <option value={originCountry}>{originCountry.toUpperCase()}</option>)}
                                    </select>
                                </div>
                                <button type="button" className="btn btn-primary filterbutton ml-1" title="Filter" onClick={this.handleFiltration}><FontAwesomeIcon icon={faFilter} /> </button>

                            </div>
                            <div style={{ marginTop: "10px" }} className="row">
                                <div className="col-1"></div>
                                <label className="col-2 col-form-label">Tracking Number</label>
                                <div className="col-5">
                                    <select disabled={this.state.trackingList.length === 0} value={this.state.trackingNbr} className="form-control" name="trackingNbr" onChange={this.handletrackingNbrChange} >
                                        <option>Select</option>
                                        {this.state.trackingList.map((tNbr) => <option value={tNbr}>{tNbr}</option>)}
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div className="card" >

                        <div className="card-body">

                            <div style={{ marginTop: "10px" }} className="row">
                                <div className="col-1"></div>
                                <label className="col-3 col-form-label">Length/Width/Height/Dimension Unit</label>
                                <div className="col-1">
                                    <input type="text" className="form-control" name="length" maxlength="2" onChange={this.handleChange} />
                                </div>
                                <div className="col-1">
                                    <input type="text" className="form-control" name="width" maxlength="2" onChange={this.handleChange} />
                                </div>
                                <div className="col-1">
                                    <input type="text" className="form-control" name="height" maxlength="2" onChange={this.handleChange} />
                                </div>
                                <div className="col-2">
                                    <select className="form-control" name="dimUnit" onChange={this.handleChange} >
                                        <option>Select</option>
                                        <option value='IN'>IN</option>
                                        <option value='CM'>CM</option>

                                    </select>
                                </div>
                            </div>
                            <div style={{ marginTop: "10px" }} className="row">
                                <div className="col-1"></div>
                                <label className="col-3 col-form-label">Weight/Weight Unit</label>
                                <div className="col-1">
                                    <input type="text" className="form-control" name="weight" maxlength="2" onChange={this.handleChange} />
                                </div>

                                <div className="col-2">
                                    <select className="form-control" name="weightUnit" onChange={this.handleChange} >
                                        <option>Select</option>
                                        <option value='KG'>KG</option>
                                        <option value='LB'>LB</option>
                                        <option value='G'>G</option>
                                        <option value='OZ'>OZ</option>
                                    </select>
                                </div>
                            </div>

                            <div style={{ marginTop: "10px" }} className="row">
                                <div class="col-md-12 text-center">
                                    <button title="Add Rate" className="btn btn-primary" onClick={this.SaveRate} >Add Adjustment Rate</button>
                                </div>
                            </div>
                        </div>


                    </div>

                </div>
            </>
        );
    }
}


export default PurchaseOrder;