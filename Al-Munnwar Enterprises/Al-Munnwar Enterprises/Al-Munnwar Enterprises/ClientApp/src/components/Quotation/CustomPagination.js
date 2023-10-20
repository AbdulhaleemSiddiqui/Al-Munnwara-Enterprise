import React, { Component } from 'react';
import Pagination from 'react-bootstrap/Pagination';

import 'bootstrap/dist/css/bootstrap.css';

export class CustomPagination extends Component {
    static displayName = CustomPagination.name;
    constructor(props) {
        super(props);
        this.firstPage = this.props.firstPage ?? 1;
        this.lastPage = this.props.lastPage ?? 1;
        this.onNextClick = this.onNextClick.bind(this);
        this.onENextClick = this.onENextClick.bind(this);
        this.onLastClick = this.onLastClick.bind(this);
        this.onFirstClick = this.onFirstClick.bind(this);
        this.onPrevClick = this.onPrevClick.bind(this);
        this.onEPrevClick = this.onEPrevClick.bind(this);
        this.onPageChange = this.onPageChange.bind(this);

        this.state = {
            pageSize: this.props.pageSize,
            totalRecords: this.props.totalRecords,
            currentPage: this.props.currentPage
        };
    }


    pager(currentPage) {
        let active = currentPage;
        this.state.totalPages = Math.ceil(this.props.totalRecords / this.props.pageSize);
        let items = [];

        if (this.state.totalPages <= 10) {
            this.firstPage = 1;
            this.lastPage = this.state.totalPages;
            if (currentPage != 1) {
                items.push(<Pagination.Prev onClick={this.onPrevClick} />);
            }
            for (let number = 1; number <= this.state.totalPages; number++) {
                items.push(
                    <Pagination.Item key={number} active={number === active} onClick={this.onPageChange}>
                        {number}
                    </Pagination.Item>,
                );
            }
            if (currentPage != this.state.totalPages && this.state.totalPages > 0) {
                items.push(<Pagination.Next onClick={this.onNextClick} />);
            }
        }
        else if (currentPage <= 10) {
            this.firstPage = 1;
            this.lastPage = 10;
            if (currentPage != 1) {
                items.push(<Pagination.Prev onClick={this.onPrevClick} />);
            }
            for (let number = 1; number <= 10; number++) {
                items.push(
                    <Pagination.Item key={number} active={number === active} onClick={this.onPageChange}>
                        {number}
                    </Pagination.Item>,
                );
            }
            items.push(<Pagination.Ellipsis onClick={this.onENextClick} />);
            items.push(<Pagination.Item key={this.state.totalPages} active={this.state.totalPages == active} onClick={this.onLastClick}>{this.state.totalPages}</Pagination.Item>);
            items.push(<Pagination.Next onClick={this.onNextClick} />);
        }
        else if (currentPage > this.state.totalPages - 10) {
            this.firstPage = this.state.totalPages - 9;
            this.lastPage = this.state.totalPages;
            items.push(<Pagination.Prev onClick={this.onPrevClick} />);
            items.push(<Pagination.Item key={1} active={1 === active} onClick={this.onFirstClick}>{1}</Pagination.Item>);
            items.push(<Pagination.Ellipsis onClick={this.onEPrevClick} />);

            for (let number = this.state.totalPages - 9; number <= this.state.totalPages; number++) {
                items.push(
                    <Pagination.Item key={number} active={number === active} onClick={this.onPageChange}>
                        {number}
                    </Pagination.Item>,
                );
            }
            if (currentPage != this.state.totalPages && this.state.totalPages > 0) {
                items.push(<Pagination.Next onClick={this.onNextClick} />);
            }
        }
        else {
            //show first, back, next, last
            items.push(<Pagination.Prev onClick={this.onPrevClick} />);
            items.push(<Pagination.Item key={1} active={1 === active} onClick={this.onFirstClick}>{1}</Pagination.Item>);
            items.push(<Pagination.Ellipsis onClick={this.onEPrevClick} />);

            for (let number = this.firstPage; number <= this.lastPage; number++) {
                items.push(
                    <Pagination.Item key={number} active={number === active} onClick={this.onPageChange}>
                        {number}
                    </Pagination.Item>,
                );
            }
            items.push(<Pagination.Ellipsis onClick={this.onENextClick} />);
            items.push(<Pagination.Item key={this.state.totalPages} active={this.state.totalPages === active} onClick={this.onLastClick}>{this.state.totalPages}</Pagination.Item>);
            items.push(<Pagination.Next onClick={this.onNextClick} />);
        }

        return items;
    }

    onPageChange(e) {
        let currentPage = parseInt(e.target.text);
        if (currentPage == this.props.currentPage || isNaN(currentPage)) {
            return;
        }
        this.setState({ currentPage: currentPage });
        this.props.setPages(currentPage, this.firstPage, this.lastPage);
        this.props.recordsHandler(currentPage);
    }

    onFirstClick() {
        let currentPage = 1;
        this.setState({ currentPage: currentPage });
        this.props.setPages(currentPage, this.firstPage, this.lastPage);
        this.props.recordsHandler(currentPage);
    }

    onEPrevClick() {
        let currentPage = 1;
        if (this.firstPage > 10) {
            currentPage = this.firstPage - 10;
        }
        this.firstPage = currentPage;
        this.lastPage = Math.min(currentPage + 9, this.state.totalPages);
        this.setState({ currentPage: currentPage });
        this.props.setPages(currentPage, this.firstPage, this.lastPage);
        this.props.recordsHandler(currentPage);
    }

    onENextClick() {
        let currentPage = this.lastPage + 1;
        this.firstPage = currentPage;
        this.lastPage = Math.min(currentPage + 9, this.state.totalPages);

        this.setState({ currentPage: currentPage });
        this.props.setPages(currentPage, this.firstPage, this.lastPage);
        this.props.recordsHandler(currentPage);
    }

    onPrevClick() {
        let currentPage = this.props.currentPage - 1;
        if (this.props.currentPage == this.firstPage) {
            this.lastPage = currentPage;
            this.firstPage = Math.max(currentPage - 9, 1);
        }
        this.setState({ currentPage: currentPage });
        this.props.setPages(currentPage, this.firstPage, this.lastPage);
        this.props.recordsHandler(currentPage);
    }

    onNextClick() {
        let currentPage = this.props.currentPage + 1;
        if (this.props.currentPage == this.lastPage) {
            this.firstPage = currentPage;
            this.lastPage = Math.min(currentPage + 9, this.state.totalPages);
        }
        this.setState({ currentPage: currentPage });
        this.props.setPages(currentPage, this.firstPage, this.lastPage);
        this.props.recordsHandler(currentPage);
    }

    onLastClick() {
        let currentPage = this.state.totalPages;
        this.setState({ currentPage: currentPage });
        this.props.setPages(currentPage, this.firstPage, this.lastPage);
        this.props.recordsHandler(currentPage);
    }

    render() {
        return (
    
                <Pagination bsPrefix='page-link'>{this.pager(this.props.currentPage)}</Pagination>
            
        );
    }
}