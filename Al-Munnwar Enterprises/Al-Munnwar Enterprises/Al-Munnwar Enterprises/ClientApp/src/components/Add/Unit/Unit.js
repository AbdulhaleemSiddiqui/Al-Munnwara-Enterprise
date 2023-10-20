import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrashAlt, faMinusCircle, faFilter, faPlusCircle} from '@fortawesome/free-solid-svg-icons'
import { CustomPagination } from '../Unit/CustomPagination'
import Modal from 'react-bootstrap/Modal';
export class Unit extends Component {
    constructor(props) {
        super(props);

        this.handleShow = this.handleShow.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.state = {
            pageSize: 10,
            totalRecords: 0,
            currentPage: 1,
            isSortAsc: true,
            sortingBy:"name",
            UnitList: [],
            id: 0,
            name: '',
            message: "",
            shipperAcctsList: [],
            modalCreateNewUnit: false,
            setShow: false,
            errorMessage: '',
            shipperList: [],
           
          
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
            showFilters: 'none',
            showFilterLabel: 'Show Filters'

        }
        this.firstPage = 1;
        this.lastPage = 1;
    }
    async componentDidMount() {
        await this.getUnit();
        window.scrollTo({ top: 0, behavior: "smooth" });
    }
    async getUnit(e) {

        try {
            const result = await fetch('api/GetUnit', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    name: this.state.name,
                    pageSize: this.state.pageSize,
                    pageName: 'Unit',
                    pageNumber: this.state.currentPage,
                    sortBy: this.state.sortingBy,
                    isSortAsc: this.state.isSortAsc
                })

            })
            const json = await result.json();
            if (result.status === 200) {
                if (json.unit && json.unit.length) {
                    this.setState({
                        UnitList: json.unit,
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
                        UnitList: [],
                        totalRecords:0
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

    async DeleteUnit(id) {
        console.log(id);
        try {
            const response = await fetch(`api/DeleteUnit?id=${id}`, {
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
                    Message: "Unit Deleted",
                    isSuccess: true,
                    errorMessage: '',
                }, () => { this.getUnit(); });

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

    SaveUnit = async () => {
        try {
            const result = await fetch("api/SaveUnit", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    name: this.state.name,
                    price: this.state.price,

                })
            })
            const response = await result.json();
            if (response === "ok") {
                this.setState({
                    isError: false,
                    Message: "Unit  Saved",
                    isSuccess: true,
                    errorMessage: '',

                }, () => { this.handleClose(); this.getUnit() });
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
    UpdateUnit = async () => {
        try {
            const result = await fetch("api/UpdateUnit", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    name: this.state.name,
                    price: this.state.price,
                    vId: this.state.vId,
                    uId: this.state.uId,
                    id: this.state.id,

                })
            })
            if (!result.ok) {
                throw new Error(`Fetch failed with status ${result.status}`);
            }
            const response = await result.json();
            console.log('Response:', response);

            if (response === "ok") {
                this.setState({
                    isError: false,
                    Message: "Unit  Updated",
                    isSuccess: true,
                    errorMessage: '',

                }, () => { this.handleClose(); this.getUnit() });
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
    handleShow() {
        this.setState({ setShow: true });
    }
    handleSave = () => {
        if (this.state.id > 0) {
            this.UpdateUnit();
        }
        else {
            this.SaveUnit();
        }
    }
    handleClose() {
        this.setState({
            setShow: false,
            name: "",
            id: ""
        });
    };

    handleDelete(id) {
        this.setState({}, () => { this.DeleteUnit(id) });
    }


    toggle = () => {
        this.setState({ modalCreateNewUnit: true })
    }

    handleEdit = (Unit) => {
        this.setState({
            name: Unit.name,
            id: Unit.id
        });
 
    }
    handleChange = (e) => {
        this.setState({
            [e.target.name]: e.target.value
        });
    }

    getNameByKeyPress = async (event) => {
        var code = event.keyCode || event.which;
        if (code === 13) { //13 is the enter keycode
            event.preventDefault();
            this.setState({ currentPage: 1 });
            await this.getUnit(1);
        }
    }
    handleFiltration = async (event) => {
        event.preventDefault();
        this.setState({ currentPage: 1 });
        await this.getUnit(1);
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
            isSortAsc:!this.state.isSortAsc
        }, () => { this.getUnit(1) });

    }

    renderTableData() {
        return this.state.UnitList.map((Unit, index) => {
            const { id, name} = Unit
            return (
                <tr>
                    <td class="col-2">{name}</td>
                   
                    <td class="col-2">
                        <button class="btn btn-primary" style={{ marginRight: '10px' }} to={this.props.myroute} title="Edit" onClick={() => { this.handleEdit(Unit) }} ><FontAwesomeIcon icon={faEdit} /></button>
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
                    <button onClick={this.handleShow} class="btn btn-primary me-md-2" type="button"> Create Unit</button>
                </div>
                <div>
                    <fieldset className="row border p-2" >
                        <legend className="w-auto" style={{ fontSize: "1.0rem" }}> {this.renderFilterButton()}</legend>
                        <div style={{ display: this.state.showFilters }}>
                            <form className="form-inline">
                                <div className="col-12 d-flex" style={{ marginBottom: "5px", paddingLeft: "0px" }}>
                                    <table>
                                        <tr>
                                            <td><label for="filterrecord" style={{ width: "inherit" }} class="p-2">Unit Name</label></td>
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
                                <th class="col-2" style={{ display: this.state.NoRecordStyle }} onClick={() => this.sorting('name')}>Name</th>
                                <th class="col-2" style={{ display: this.state.NoRecordStyle, cursor: "default", minWidth: "120px" }}>Actions</th>
                                {this.state.NoRecordErrorStyle != '' ? <td style={{ textAlign: "center" }}><b>{this.state.NoRecordError}</b></td> : null}
                            </tr>
                        </thead>
                        <tbody>
                            {this.renderTableData()}
                        </tbody>
                    </table >

                </div>

                <Modal show={this.state.setShow} onHide={this.handleClose} className="Large-Modal">
                    <Modal.Header>

                        <Modal.Title>ADD Unit</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>

                        <div className="row">
                            <div className="col-2"></div>
                            <div className="col-3">Name</div>
                            <div className="col-5" style={{ display: 'flex' }}><input value={this.state.name} style={{ flexGrow: 1, float: 'left' }} type="text" maxLength="50" className="form-control mr-2" name="name" onChange={this.handleChange} /> <span style={{ flexGrow: 1, float: 'left', color: 'red' }}>*</span></div>
                            <div className="col-2"></div>
                        </div>
                        <br />
                       
                    </Modal.Body>
                    <Modal.Footer>
                        <button color="primary" className="btn btn-primary" title="Save" onClick={this.handleSave}>Save</button>
                        <button color="Secondary" className="btn btn-secondary" title="Cancel" onClick={this.handleClose}>Cancel</button>
                    </Modal.Footer>
                </Modal>
                <br/>
                <div class="d-flex-justify-content-between div margin adjust-15">
                    <div className="justify-content-start overflow-auto">

                        {this.state.totalRecords > 0 ? <CustomPagination

                            pageSize={this.state.pageSize}
                            totalRecords={this.state.totalRecords}
                            currentPage={this.state.currentPage}
                            firstPage={this.firstPage}
                            lastPage={this.lastPage}
                            recordsHandler={(e) => { this.getUnit(e) }}
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