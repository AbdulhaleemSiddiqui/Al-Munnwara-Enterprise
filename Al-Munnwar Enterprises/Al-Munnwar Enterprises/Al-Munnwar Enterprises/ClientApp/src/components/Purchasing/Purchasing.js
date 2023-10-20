import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrashAlt, faMinusCircle, faFilter, faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { CustomPagination } from '../../CustomPagination';
import Modal from 'react-bootstrap/Modal';

export default  function Purchasing() {
    const [pageSize, setPageSize] = useState(10);
    const [totalRecords, setTotalRecords] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [isSortAsc, setIsSortAsc] = useState(true);
    const [sortingBy, setSortingBy] = useState('name');
    const [itemList, setItemList] = useState([]);
    const [purchaseList, setPurchaseList] = useState([]);
    const [id, setId] = useState(0);
    const [Message, setMessage] = useState('');
    const [showFilters, setShowFilters] = useState('none');
    const [showFilterLabel, setShowFilterLabel] = useState('Show Filters');
    const [errorMessage, setErrorMessage] = useState('');
    const [NoRecordStyle, setNoRecordStyle] = useState('');
    const [NoRecordError, setNoRecordError] = useState('');
    const [NoRecordErrorStyle, setNoRecordErrorStyle] = useState('');
    const [ItemName, setItemName] = useState(0);
    const [ItemPrice, setItemPrice] = useState(0);
    const [Expancename, setExpancename] = useState(0);
    const [ExpanceAmount, setExpanceAmount] = useState(0);
    const [isSuccess, setIsSuccess] = useState(false);
    const [isError, setIsError] = useState(false);

    const [isShow, setShow] = useState(false);

    const firstPage = 1;
    const lastPage = 1;

    useEffect(() => {
        GetPurchase();
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }, []);

    const GetItem = async (e) => {

        try {
            const result = await fetch('api/GetPurchase', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    Expancename: Expancename,
                    pageSize: pageSize,
                    pageName: 'Purchase',
                    pageNumber: currentPage,
                    sortBy: sortingBy,
                    isSortAsc: isSortAsc
                })

            })
            const json = await result.json();
            if (result.status === 200) {
                if (json.item && json.item.length) {
                    setItemList(json.item);
                    setTotalRecords(json.totalRecords),
                        setIsError(false),
                        setNoRecordError(''),
                        setNoRecordStyle(''),
                        setoRecordErrorStyle('');

                }
                else {

                    setIsError(false),
                        setNoRecordError('No Records Found'),
                        setNoRecordStyle('none'),
                        setoRecordErrorStyle('500px');
                    setItemList([]);
                    setTotalRecords(0);
                }
            }
            else if (result.status == 401) {
                setMessage(json);
                setIsError(true);
            }
            else {
                setMessage(result.statusText || "Unable to get tracking Nbr");
                setIsError(true);
            }
        }
        catch (error) {
        }
    }

    const GetPurchase = async (e) => {

        try {
            const result = await fetch('api/GetPurchase', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    Expancename: Expancename,
                    pageSize: pageSize,
                    pageName: 'Purchase',
                    pageNumber: currentPage,
                    sortBy: sortingBy,
                    isSortAsc: isSortAsc
                })

            })
            const json = await result.json();
            if (result.status === 200) {
                if (json.item && json.item.length) {
                    setPurchaseList(json.item);
                    setTotalRecords(json.totalRecords),
                        setIsError(false),
                        setNoRecordError(''),
                        setNoRecordStyle(''),
                        setoRecordErrorStyle('');

                }
                else {

                    setIsError(false),
                        setNoRecordError('No Records Found'),
                        setNoRecordStyle('none'),
                        setoRecordErrorStyle('500px');
                    setItemList([]);
                    setTotalRecords(0);
                }
            }
            else if (result.status == 401) {
                setMessage(json);
                setIsError(true);
            }
            else {
                setMessage(result.statusText || "Unable to get tracking Nbr");
                setIsError(true);
            }
        }
        catch (error) {
        }
    }

    const DeleteItem = async (id) => {
        console.log(id);
        try {
            const response = await fetch(`api/DeleteItem?id=${id}`, {
                method: 'Get',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },

            })
            const json = await response.json();
            if (json === "ok") {
                setItemList(json.item);
                setTotalRecords(json.totalRecords),
                    setIsError(false),
                    setNoRecordError(''),
                    setNoRecordStyle(''),
                    setoRecordErrorStyle('');
                setMessage("Purchase Deleted");
                setIsSuccess(true);
                setErrorMessage("");
                await GetItem();
            }
            else if (result.status == 401) {
                setMessage(json);
                setIsError(true);
            }
            else {
                setMessage(result.statusText || "Unable to get tracking Nbr");
                setIsError(true);
            }
        }
        catch (error) {
        }
    }

    const SavePurchase = async () => {
        try {
            const result = await fetch("api/SavePurchase", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    ItemName: ItemName,
                    ItemPrice: ItemPrice,
                    Expancename: Expancename,
                    experinceAmount: ExpanceAmount,

                })
            })
            const response = await result.json();
            if (response === "ok") {
                setItemList(json.item);
                setTotalRecords(json.totalRecords),
                    setIsError(false),
                    setMessage("Purchase Added"),
                    setIsSuccess(true),
                    setErrorMessage("");
                     await handleClose();
                await GetItem();

            }
            else if (response === null) {
             
            }
            scrollToTop();
        }
        catch (e) {

        }

    }
    const UpdatePurchase = async () => {
        try {
            const result = await fetch("api/UpdatePurchase", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    ItemName: ItemName,
                    ItemPrice: ItemPrice,
                    Expancename: Expancename,
                    ExpanceAmount: ExpanceAmount
                })
            })
            if (!result.ok) {
                throw new Error(`Fetch failed with status ${result.status}`);
            }
            const response = await result.json();
            console.log('Response:', response);

            if (response === "ok") {
                setIsError();
                setMessage();
                setIsSuccess();
                setErrorMessage();
       
                    await  handleClose();
                await GetItem();

            }
            else if (response === null) {
               
            }
           scrollToTop();
        }
        catch (e) {

        }

    }



    // Define the functions for fetching data, handling state, and UI logic as needed.
    const setFilterView = () => {
        if (showFilters === 'none') {
            setShowFilters('block');
            setShowFilterLabel('Hide Filters');
        } else {
            setShowFilters('none');
            setShowFilterLabel('Show Filters');
        }
    };

    const handleSave = () => {
        if (id > 0) {
            UpdateItem();
        } else {
            SaveItem();
        }
    };

    const handleClose = () => {
        setShow(false);

    };

    const handleDelete = (id) => {
        // Implement your delete logic here.
    };

    const handleEdit = (item) => {
        setName(item.name);
        setUnit(item.unit.name);
        setUId(item.unit.id);
        setPrice(item.price);
        setVName(item.vendor.name);
        setVId(item.vendor.id);
        setId(item.id);
        setShow(true);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        switch (name) {
            case 'name':
                setName(value);
                break;
            case 'price':
                setPrice(value);
                break;
            // Add more cases for other fields as needed.
            default:
                break;
        }
    };

    const getNameByKeyPress = async (event) => {
        if (event.keyCode === 13) {
            event.preventDefault();
            setCurrentPage(1);
            await GetPurchase(1);
        }
    };

    const handleFiltration = async (event) => {
        event.preventDefault();
        setCurrentPage(1);
        await GetPurchase(1);
    };

    const renderFilterButton = () => {
        if (showFilters === 'none') {
            return (
                <button
                    type="button"
                    style={{ border: 'none', width: '120px', backgroundColor: '#00000000', color: "#39A2DA", fontSize: "1.0rem", textAlign: "left" }}
                    title="Click to show filters"
                    onClick={setFilterView}
                >
                    <FontAwesomeIcon icon={faPlusCircle} /> {showFilterLabel}
                </button>
            );
        } else {
            return (
                <button
                    type="button"
                    style={{ border: 'none', width: '120px', backgroundColor: '#00000000', color: "#39A2DA", fontSize: "1.0rem", textAlign: "left" }}
                    title="Click to hide filters"
                    onClick={setFilterView}
                >
                    <FontAwesomeIcon icon={faMinusCircle} /> {showFilterLabel}
                </button>
            );
        }
    };

    const sorting = (value) => {
        setSortingBy(value);
        setIsSortAsc(!isSortAsc);
        GetPurchase(1);
    };

    const renderTableData = () => {
        return purchaseList.map((item, index) => {
            const { id, name, price, date } = item;
            return (
                <tr key={id}>
                    <td className="col-2">{name}</td>
                    <td className="col-1">{price}</td>
                    <td className="col-2">{item.vendor.name}</td>
                    <td className="col-1">{item.unit.name}</td>
                    <td className="col-2">{date}</td>
                    <td className="col-2">
                        <button className="btn btn-primary" style={{ marginRight: '10px' }} title="Edit" onClick={() => handleEdit(item)}>
                            <FontAwesomeIcon icon={faEdit} />
                        </button>
                        <button className="btn btn-danger" style={{ marginRight: '10px' }} title="Delete" onClick={() => handleDelete(id)}>
                            <FontAwesomeIcon icon={faTrashAlt} />
                        </button>
                    </td>
                </tr>
            );
        });
    };
    const handleShow = () => {
        setShow(true);
        GetItem();
    }
    return (
        <div>
            <div className={isError ? "alert alert-danger" : ""} >
                {errorMessage}
            </div>
            <div className={isSuccess ? "alert alert-success" : ""} >
                {Message}
            </div>
            <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                <button onClick={handleShow} class="btn btn-primary me-md-2" type="button"> Create Item</button>
            </div>
            <div>
                <fieldset className="row border p-2" >
                    <legend className="w-auto" style={{ fontSize: "1.0rem" }}> {renderFilterButton()}</legend>
                    <div style={{ display: showFilters }}>
                        <form className="form-inline">
                            <div className="col-12 d-flex" style={{ marginBottom: "5px", paddingLeft: "0px" }}>
                                <table>
                                    <tr>
                                        <td><label for="filterrecord" style={{ width: "inherit" }} class="p-2">Item Name</label></td>
                                        <td>
                                            <div style={{ marginRight: "4px" }}>
                                                <input type="text" className="form-control" id="filterrecord" name="name" value={name} onChange={(e) => handleChange(e)} onKeyPress={(e) => getNameByKeyPress(e)} />
                                            </div>
                                        </td>
                                        <td >
                                            <button type="button" className="btn btn-primary filterbutton ml-1" title="Filter" onClick={handleFiltration}><FontAwesomeIcon icon={faFilter} /> </button>
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
                            <th class="col-2" style={{ display: NoRecordStyle }} onClick={() => sorting('ItemName')}>Item Name</th>
                            <th class="col-1" style={{ display: NoRecordStyle }} onClick={() => sorting('Itemprice')}>Item Price</th>
                            <th class="col-2" style={{ display: NoRecordStyle }} onClick={() => sorting('ExpanceName')}>Expance Name</th>
                            <th class="col-1" style={{ display: NoRecordStyle }} onClick={() => sorting('ExpanceAmount')}>Expance Amountt</th>
                            <th class="col-2" style={{ display: NoRecordStyle }} onClick={() => sorting('date')}>Date</th>
                            <th class="col-2" style={{ display: NoRecordStyle, cursor: "default", minWidth: "120px" }}>Actions</th>
                            {NoRecordErrorStyle != '' ? <td style={{ textAlign: "center" }}><b>{NoRecordError}</b></td> : null}
                        </tr>
                    </thead>
                    <tbody>
                        {renderTableData()}
                    </tbody>
                </table >

            </div>
            <Modal show={isShow} onHide={handleClose} className="Large-Modal">
                <Modal.Header>

                    <Modal.Title>Add Purchase</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className="row">
                        <div className="col-1"></div>
                        <div className="col-4">Item Name</div>
                        <div className="col-5" style={{ display: 'flex' }}>
                            <select style={{ flexGrow: 1, float: 'left' }} type="text" maxLength="50" className="form-control mr-2" name="ItemName" onChange={handleChange} >
                                <option value={ItemName}>{ItemName !== "" ? ItemName : "Select"}</option>
                                {itemList.map((v, index) => <option value={v.name}>{v.name + '-' + v.price}</option>)}
                            </select>
                            <span style={{ flexGrow: 1, float: 'left', color: 'red' }}>*</span>
                        </div>
                        <br />
                    </div>
                    <div className="row">
                        <div className="col-1"></div>
                        <div className="col-4">Expense Name</div>
                        <div className="col-5" style={{ display: 'flex' }}><input value={Expancename} style={{ flexGrow: 1, float: 'left' }} type="text" maxLength="50" className="form-control mr-2" name="name" onChange={handleChange} /> <span style={{ flexGrow: 1, float: 'left', color: 'red' }}>*</span></div>
                        <div className="col-2"></div>
                    </div>
                    <br />
                    <div className="row">
                        <div className="col-1"></div>
                        <div className="col-4">Item Price</div>
                        <div className="col-5" style={{ display: 'flex' }}><input value={ItemPrice} style={{ flexGrow: 1, float: 'left' }} type="text" maxLength="50" className="form-control mr-2" name="price" onChange={handleChange} /> <span style={{ flexGrow: 1, float: 'left', color: 'red' }}>*</span></div>
                        <div className="col-2"></div>
                    </div>
                    <br />

                    <br />

                </Modal.Body>
                <Modal.Footer>
                    <button color="primary" className="btn btn-primary" title="Save" onClick={handleSave}>Save</button>
                    <button color="Secondary" className="btn btn-secondary" title="Cancel" onClick={handleClose}>Cancel</button>
                </Modal.Footer>
            </Modal>
            <br />
            <div className="d-flex-justify-content-between div margin adjust-15">
                <div className="justify-content-start overflow-auto">
                    {totalRecords > 0 ? (
                        <CustomPagination
                            pageSize={pageSize}
                            totalRecords={totalRecords}
                            currentPage={currentPage}
                            firstPage={firstPage}
                            lastPage={lastPage}
                            recordsHandler={(e) => {
                                setCurrentPage(e);
                                GetPurchase(e);
                            }}
                            setPages={(currentPage, firstPage, lastPage) => {
                                setCurrentPage(currentPage);
                                // You can update firstPage and lastPage here if needed.
                            }}
                        />
                    ) :
                        null}
                </div>
            </div>
        </div>);
}

