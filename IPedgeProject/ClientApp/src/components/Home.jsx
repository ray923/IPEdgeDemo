import React, { Component } from 'react';
import axios from 'axios';

export class Home extends Component {
    constructor(props) {
        super(props);

        this.onEmployeeUpdate = this.onEmployeeUpdate.bind(this);
        this.onEmployeeDelete = this.onEmployeeDelete.bind(this);
        this.populatePagedEmployeesData = this.populatePagedEmployeesData.bind(this);

        this.state = {
            employees: [],
            loading: true,
            failed: false,
            error: '',
            pagesize: 10
        }
    }

    componentDidMount() {
        this.populatePagedEmployeesData(1);
    }

    onEmployeeUpdate(id) {
        const { history } = this.props;
        history.push('/update/' + id);
    }

    onEmployeeDelete(id) {
        const { history } = this.props;
        history.push('/delete/' + id);
    }

    populateEmployeeData() {
        axios.get("/Employee").then(result => {
            const response = result.data;
            this.setState({ employees: response, loading: false, failed: false, error: "" });
        }).catch(error => {
            this.setState({ employees: [], loading: false, failed: true, error: "Employees could not be loaded" });
        });
    }

    populatePagedEmployeesData(pageindex) {
        axios.get("/Employee/" + pageindex + "/" + this.state.pagesize).then(result => {
            const response = result.data;
            this.setState({ employees: response, loading: false, failed: false, error: "" });
        }).catch(error => {
            this.setState({ employees: [], loading: false, failed: true, error: "Employees could not be loaded" });
        });
    }

    renderPaged(employees){
        let rows = [];
        for (let i=1; i<= employees.pageCount + 1; i++)
        {
            rows.push(<li className="page-item"><a className="page-link" onClick={() => this.populatePagedEmployeesData(i)}>{i}</a></li>);
        }
        return (
            <>
            <nav aria-label="Page navigation example">
                <ul className="pagination">
                    <li className="page-item">
                        <a className="page-link" onClick={() => this.populatePagedEmployeesData(employees.pageIndex-1 >0 ? employees.pageIndex-1 : 1)} aria-label="Previous">
                            <span aria-hidden="true">&laquo;</span>
                            <span className="sr-only">Previous</span>
                        </a>
                    </li>
                        {rows}
                    <li className="page-item">
                        <a className="page-link" onClick={() => this.populatePagedEmployeesData(employees.pageIndex + 1 >= employees.pageCount +1 ? employees.pageCount +1 : employees.pageCount)} aria-label="Next">
                            <span aria-hidden="true">&raquo;</span>
                            <span className="sr-only">Next</span>
                        </a>
                    </li>
                </ul>
            </nav>
            <div className="dropdown dropright btn-sm">
                <button className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    PageSize
                </button>
                <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <a className="dropdown-item" onClick={() => this.setState({pagesize:5})}>5</a>
                    <a className="dropdown-item" onClick={() => this.setState({pagesize:10})}>10</a>
                    <a className="dropdown-item" onClick={() => this.setState({pagesize:50})}>50</a>
                </div>
            </div>
            </>
        )
    }

    renderAllEmployeesTable(employees) {
        return (
            <>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>EmployeeNumber</th>
                        <th>FirstName</th>
                        <th>LastName</th>
                        <th>Date Joined</th>
                        <th>Extension</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        employees.employees.map(employee => (
                            <tr key={employee.employeeID}>
                                <td>{employee.employeeNumber}</td>
                                <td>{employee.firstName}</td>
                                <td>{employee.lastName}</td>
                                <td>{new Date(employee.dateJoined).toISOString().slice(0, 10)}</td>
                                <td>{employee.extension}</td>
                                <td>
                                    <div className="form-group">
                                        <button onClick={() => this.onEmployeeUpdate(employee.employeeID)} className="btn btn-success">
                                            Update
                                    </button>
                                        <button onClick={() => this.onEmployeeDelete(employee.employeeID)} className="btn btn-danger">
                                            Delete
                                    </button>
                                    </div>
                                </td>
                            </tr>
                        ))
                    }
                </tbody>                
            </table>
            {this.renderPaged(employees)}
            </>
        );
    }

    render() {

        let content = this.state.loading ? (
            <p>
                <em>Loading...</em>
            </p>
        ) : (this.state.failed ? (
            <div className="text-danger">
                <em>{this.state.error}</em>
            </div>
        ) : (
                this.renderAllEmployeesTable(this.state.employees))
            )

        return (
            <div>
                <h1>All Employees</h1>
                <p>Here you can see all Employees</p>
                {content}
            </div>
        );
    }
}