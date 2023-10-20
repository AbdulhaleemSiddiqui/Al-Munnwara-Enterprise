import React, { Component, Fragment } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faPrint,faTrashAlt, faMinusCircle, faFilter, faPlusCircle } from '@fortawesome/free-solid-svg-icons'
import { CustomPagination } from '../Quotation/CustomPagination'
import Modal from 'react-bootstrap/Modal';
import { PDFViewer } from '@react-pdf/renderer'
import Invoice from '../Quotation/reports/Invoice'


export class Quotation extends Component {
    constructor(props) {
        super(props);

        this.handleShow = this.handleShow.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.state = {
            setprint:false,
            pageSize: 10,
            totalRecords: 0,
            currentPage: 1,
            isSortAsc: true,
            sortingBy: "name",
            quotationList: [],
            Invoice:[],
            iList: [],
            tList:[],
            id: 0,
            iId:0,
            uId: 0,
            tId: 0,
            qty: 0,
            rate: 0,
            type: "",
            status: false,
            iName: "",
            uName: "",
            taxName: '',
            date: '',
            unit: '',
            message: "",
            setShow: false,
            errorMessage: '',
            Message: '',
            isError: false,
            NoRecordStyle: '',
            NoRecordError: '',
            NoRecordErrorStyle: '',
            isSuccess: false,
            showFilters: 'none',
            showFilterLabel: 'Show Filters'

        }
        this.firstPage = 1;
        this.lastPage = 1;
    }
    async componentDidMount() {
        await this.getQuotation();
        window.scrollTo({ top: 0, behavior: "smooth" });
    }
 
    async getTax() {

        try {
            const result = await fetch('api/GetTax', {
                method: 'Get',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },

            })
            const json = await result.json();
            if (result.status === 200) {
                if (json.length) {
                    this.setState({
                        tList: json,
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
                        tList: [],
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
                    Message: result.statusText || "Unable to get tax ",
                    isError: true
                })
            }
        }
        catch (error) {
        }
    }
    async getItem() {

        try {
            const result = await fetch('api/GetItem', {
                method: 'Get',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },

            })
            const json = await result.json();
            if (result.status === 200) {
                if (json.length) {
                    this.setState({
                        iList: json,
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
                        iList: [],
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

    async getQuotation(e) {

        try {
            const result = await fetch('api/getQuotation', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    name: this.state.name,
                    vendorName: this.state.vendorName,
                    pageSize: this.state.pageSize,
                    pageName: 'quotation',
                    pageNumber: this.state.currentPage,
                    sortBy: this.state.sortingBy,
                    isSortAsc: this.state.isSortAsc
                })

            })
            const json = await result.json();
            if (result.status === 200) {
                if (json.quotation && json.quotation.length) {
                    this.setState({
                        quotationList: json.quotation,
                        totalRecords: json.totalRecords,
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
                        quotationList: [],
                        totalRecords: 0
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
                    Message: result.statusText || "Unable to get Quotation",
                    isError: true
                })
            }
        }
        catch (error) {
        }
    }

    async Deletequotation(id) {
        console.log(id);
        try {
            const response = await fetch(`api/Deletequotation?id=${id}`, {
                method: 'Get',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },

            })
            const json = await response.json();
            if (json === "ok") {

                this.setState({
                    isError: false,
                    NoRecordError: '',
                    NoRecordStyle: '',
                    NoRecordErrorStyle: '',
                    isError: false,
                    Message: "Item Deleted",
                    isSuccess: true,
                    errorMessage: '',
                }, () => { this.getQuotation(); });

            }
            else if (result.status == 401) { 
                this.setState({
                    Message: json,
                    isError: true,

                })
            }
            else {
                this.setState({
                    Message: result.statusText || "Unable to get tracking Nbr",
                    isError: true,

                })
            }
        }
        catch (error) {
        }
    }

    SaveQuotation = async () => {
        try {
            const result = await fetch("api/SaveQuotation", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    iName: this.state.iName,
                    type: this.state.type,
                    uId: this.state.uId,
                    qty: this.state.qty,
                    rate: this.state.rate,
                 /*   status: null*/

                })
            })
            const response = await result.json();
            if (response === "ok") {
                this.setState({
                    isError: false,
                    Message: "quotation  Saved",
                    isSuccess: true,
                    errorMessage: '',

                }, () => { this.handleClose(); this.getQuotation(1) });
            }
            else if (response === null) {
                this.setState({
                    isError: true,
                    errorMessage: "quotation Record Record Not Saved",
                    Message: '',
                    isSuccess: false,
                });
            }
            this.scrollToTop();
        }
        catch (e) {

        }

    }
    UpdateQuotation = async () => {
        try {
            const result = await fetch("api/UpdateQuotation", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    id: this.state.id,
                    status: this.state.status,
                    qty: this.state.qty,
                    rate: this.state.rate,
                    iName: this.state.iName,
                    uName: this.state.uName,
                    type: this.state.type,
                })
            })
    
            const response = await result.json();
            console.log('Response:', response);

            if (response === "ok") {
                this.setState({
                    isError: false,
                    Message: "Quotation  Updated",
                    isSuccess: true,
                    errorMessage: '',

                }, () => { this.handleClose(); this.getQuotation(1) });
            }
            else if (response === null) {
                this.setState({
                    isError: true,
                    errorMessage: "Record Not Saved",
                    Message: '',
                    isSuccess: false,
                });
            }
            this.scrollToTop();
        }
        catch (e) {

        }

    }
    UpdateStatus = async () => {
        try {
            const result = await fetch("api/UpdateStatus", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    id: this.state.id,
                    status: this.state.status
                })
            })

            const response = await result.json();

            if (response === "ok") {
                this.setState({
                    isError: false,
                    Message: "Status  Updated",
                    isSuccess: true,
                    errorMessage: '',

                }, () => { this.handleClose(); this.getQuotation(1) });
            }
            else if (response === null) {
                this.setState({
                    isError: true,
                    errorMessage: "Record Not Saved",
                    Message: '',
                    isSuccess: false,
                });
            }
            this.scrollToTop();
        }
        catch (e) {

        }

    }


    setFilterView = () => {
        if (this.state.showFilters === "none") {
            this.setState({
                showFilters: 'block',
                showFilterLabel: 'Hide Filters',
            })
        } else {
            this.setState({
                showFilters: 'none',
                showFilterLabel: 'Show Filters',
            })
        }
    }
    handleSave = () => {
        if (this.state.id > 0) {
            this.UpdateQuotation();
        }
        else {
            this.SaveQuotation();
        }
    }
    handleClose() {
        this.setState({
            setShow: false,
            setprint:false,
            iName: "",
            qty: 0,
            tId: 0,
            status: "",
            rate: 0,
            type: "",
            id:0,
      
        });
    };
    handleShow() {
        this.setState({ setShow: true }, () => { this.getItem(), this.getTax() });
    }
    handleDelete(id) {

        this.setState({}, () => { this.Deletequotation(id) });
    }


    toggle = () => {
        this.setState({ modalCreateNewVendor: true })
    }

    handleEdit = (quotation) => {
        this.setState({
            id:   quotation.id,
            qty:  quotation.qty,
            rate: quotation.rate,
            type: quotation.type,
            date: quotation.date,
            status: quotation.status,      
            iName: quotation.iName,
            uName: quotation.uName
        });
        this.handleShow();
    }
    handleChange = (e) => {
        this.setState({
            [e.target.name]: e.target.value
        });
    }
    handleStatusChange = (e) => {
        //this.setState({
        //    [e.target.name]: e.target.value
        //});
         this.UpdateStatus();
    }

    getNameByKeyPress = async (event) => {
        var code = event.keyCode || event.which;
        if (code === 13) { //13 is the enter keycode
            event.preventDefault();
            this.setState({ currentPage: 1 });
            await this.getQuotation(1);
        }
    }
    handleFiltration = async (event) => {
        event.preventDefault();
        this.setState({ currentPage: 1 });
        await this.getQuotation(1);
    }

    renderFilterButton() {
        if (this.state.showFilters === 'none')
            return (
                <button type="button" style={{ border: 'none', width: '120px', backgroundColor: '#00000000', color: "#39A2DA", fontSize: "1.0rem", textAlign: "left" }} title="Click to show filters" onClick={this.setFilterView}><FontAwesomeIcon icon={faPlusCircle} /> {this.state.showFilterLabel}</button>
            )
        else
            return (
                <button type="button" style={{ border: 'none', width: '120px', backgroundColor: '#00000000', color: "#39A2DA", fontSize: "1.0rem", textAlign: "left" }} title="Click to hide filters" onClick={this.setFilterView}><FontAwesomeIcon icon={faMinusCircle} /> {this.state.showFilterLabel}</button>
            )
    }
    sorting = (value) => {
        this.setState({
            sortingBy: value,
            isSortAsc: !this.state.isSortAsc
        }, () => { this.getQuotation(1) });

    }
    printInoive = (invoice) => {
        this.setState({
            Invoice: invoice,

            setprint: true
        });
    }
    renderTableData() {
        return this.state.quotationList.map((quotation, index) => {
            const { id, iName, qty, rate, type, date, uName, status } = quotation
            return (
                <tr>
                    <td class="col-1">{id}</td>
                    <td class="col-2">{iName}</td>
                    <td class="col-1">{qty}</td>
                    <td class="col-1">{rate}</td>
                    <td class="col-1">{type}</td>
                    <td class="col-1">{date}</td>
                    <td class="col-1">{uName}</td>
                    <td class="col-2">
                        <select style={{ flexGrow: 1, float: 'left' }} className="form-control mr-2" name="status" onChange={this.handleStatusChange(id)} >
                            <option value={status}>{status !== false ? "Approved" : "Rejected"}</option>
                            <option value="1"> Approved</option>
                            <option value="0"> Rejected</option>
                        </select>
                        {status}
                    </td>
                    <td class="col-2">
                        <button className="btn btn-primary" style={{ marginRight: '10px' }} title="Print"><FontAwesomeIcon icon={faPrint} onClick={() => { this.printInoive(quotation) }} /> </button >
                        <button class="btn btn-primary" style={{ marginRight: '10px' }} to={this.props.myroute} title="Edit" onClick={() => { this.handleEdit(quotation) }} ><FontAwesomeIcon icon={faEdit} /></button>
                        <button className="btn btn-danger" style={{ marginRight: '10px' }} title="Delete"><FontAwesomeIcon icon={faTrashAlt} onClick={() => { this.handleDelete(id) }} /> </button >

                    </td >
                </tr>
            )
        })
    }


    render() {

        return (

            <div>
                <div className={this.state.isError ? "alert alert-danger" : ""} >
                    {this.state.errorMessage}
                </div>
                <div className={this.state.isSuccess ? "alert alert-success" : ""} >
                    {this.state.Message}
                </div>
                <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                    <button onClick={this.handleShow} class="btn btn-primary me-md-2" type="button"> Create Item</button>
                </div>
                <div>
                    <fieldset className="row border p-2" >
                        <legend className="w-auto" style={{ fontSize: "1.0rem" }}> {this.renderFilterButton()}</legend>
                        <div style={{ display: this.state.showFilters }}>
                            <form className="form-inline">
                                <div className="col-12 d-flex" style={{ marginBottom: "5px", paddingLeft: "0px" }}>
                                    <table>
                                        <tr>
                                            <td><label for="filterrecord" style={{ width: "inherit" }} class="p-2">Item Name</label></td>
                                            <td>
                                                <div style={{ marginRight: "4px" }}>
                                                    <input type="text" className="form-control" id="filterrecord" name="name" value={this.state.name} onChange={(e) => this.handleChange(e)} onKeyPress={(e) => this.getNameByKeyPress(e)} />
                                                </div>
                                            </td>
                                            <td >
                                                <button type="button" className="btn btn-primary filterbutton ml-1" title="Filter" onClick={this.handleFiltration}><FontAwesomeIcon icon={faFilter} /> </button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </form>
                        </div>
                    </fieldset>
                </div>
                <br />
                <div class="row table-scroll table-th-nowrap">
                    <table class='table table-striped table-bordered mb-0' aria-labelledby="tableLabel">
                        <thead id="thead">
                            <tr style={{ cursor: "pointer" }}>
                                <th class="col-1" style={{ display: this.state.NoRecordStyle }} onClick={() => this.sorting('id')}>S.No</th>
                                <th class="col-2" style={{ display: this.state.NoRecordStyle }} onClick={() => this.sorting('Item')}>Item</th>
                                <th class="col-1" style={{ display: this.state.NoRecordStyle }} onClick={() => this.sorting('Qty')}>Qty</th>
                                <th class="col-1" style={{ display: this.state.NoRecordStyle }} onClick={() => this.sorting('Rate')}>Rate</th>
                                <th class="col-1" style={{ display: this.state.NoRecordStyle }} onClick={() => this.sorting('Tax')}>Tax</th>
                                <th class="col-1" style={{ display: this.state.NoRecordStyle }} onClick={() => this.sorting('Date')}>Date</th>
                                <th class="col-1" style={{ display: this.state.NoRecordStyle }} onClick={() => this.sorting('Unit')}>Unit</th>
                                <th class="col-2" style={{ display: this.state.NoRecordStyle }} onClick={() => this.sorting('Status')}>Status</th>
                                <th class="col-2" style={{ display: this.state.NoRecordStyle, cursor: "default", minWidth: "120px" }}>Actions</th>
                               {this.state.NoRecordErrorStyle != '' ? <td style={{ textAlign: "center" }}><b>{this.state.NoRecordError}</b></td> : null}
                            </tr>
                        </thead>

                        <tbody>
                            {this.renderTableData()}
                        </tbody>
                    </table >

                </div>





                <Modal show={this.state.setprint} onHide={this.handleClose} className="Large-Modal">

                    <Modal.Body>
                        <Fragment>
                            <PDFViewer width="450" height="900" className="app" >
                                <Invoice invoice={this.state.Invoice} />
                            </PDFViewer>
                        </Fragment>
                        
                    </Modal.Body>
                  
                </Modal>


































                <Modal show={this.state.setShow} onHide={this.handleClose} className="Large-Modal">
                    <Modal.Header>

                        <Modal.Title>ADD quotation</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>

                    <div className="row">
                            <div className="col-1"></div>
                            <div className="col-4">Item </div>
                            <div className="col-5" style={{ display: 'flex' }}>
                                <select style={{ flexGrow: 1, float: 'left' }} className="form-control mr-2" name="iName" onChange={this.handleChange} >
                                    <option value={this.state.iName}>{this.state.iName !== "" ? this.state.iName : "Select"}</option>
                                    {this.state.iList.map((u, index) => <option value={u.name}>{u.name}</option>)}
                                </select>
                                <span style={{ flexGrow: 1, float: 'left', color: 'red' }}>*</span>
                            </div>
                        </div>
                        <br />
                        <div className="row">
                            <div className="col-1"></div>
                            <div className="col-4">Qty</div>
                            <div className="col-5" style={{ display: 'flex' }}><input value={this.state.qty} style={{ flexGrow: 1, float: 'left' }} type="number" maxLength="50" className="form-control mr-2" name="qty" onChange={this.handleChange} /> <span style={{ flexGrow: 1, float: 'left', color: 'red' }}>*</span></div>
                            <div className="col-2"></div>
                        </div>
                        <br />
                        <div className="row">
                            <div className="col-1"></div>
                            <div className="col-4">Rate</div>
                            <div className="col-5" style={{ display: 'flex' }}><input value={this.state.rate} style={{ flexGrow: 1, float: 'left' }} type="number" maxLength="50" className="form-control mr-2" name="rate" onChange={this.handleChange} /> <span style={{ flexGrow: 1, float: 'left', color: 'red' }}>*</span></div>
                            <div className="col-2"></div>
                        </div>
                        <br />
                        <div className="row">
                            <div className="col-1"></div>
                            <div className="col-4">Tax Type</div>
                            <div className="col-5" style={{ display: 'flex' }}>
                                <select style={{ flexGrow: 1, float: 'left' }} className="form-control mr-2" name="type" onChange={this.handleChange} >
                                    <option value={this.state.type}>{this.state.type !== "" ? this.state.type : "Select"}</option>
                                    {this.state.tList.map((v, index) => <option value={v.type}>{v.type}</option>)}
                                </select>
                                <span style={{ flexGrow: 1, float: 'left', color: 'red' }}>*</span>
                            </div>
                            <br />

                        </div>
                    </Modal.Body>
                    <Modal.Footer>
                        <button color="primary" className="btn btn-primary" title="Save" onClick={this.handleSave}>Save</button>
                        <button color="Secondary" className="btn btn-secondary" title="Cancel" onClick={this.handleClose}>Cancel</button>
                    </Modal.Footer>
                </Modal>
                <br />
                <div class="d-flex-justify-content-between div margin adjust-15">
                    <div className="justify-content-start overflow-auto">

                        {this.state.totalRecords > 0 ? <CustomPagination

                            pageSize={this.state.pageSize}
                            totalRecords={this.state.totalRecords}
                            currentPage={this.state.currentPage}
                            firstPage={this.firstPage}
                            lastPage={this.lastPage}
                            recordsHandler={(e) => { this.getquotation(e) }}
                            setPages={(currentPage, firstPage, lastPage) => {
                                this.setState({ currentPage: currentPage });
                                this.firstPage = firstPage;
                                this.lastPage = lastPage;
                            }}
                        /> : null}
                    </div>
                </div>
            </div>
        );
    }
}