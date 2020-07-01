import React, { Component } from 'react';
import axios from 'axios';

export class Home extends Component {
    constructor(props) {
        super(props);

        this.onTripUpdate = this.onTripUpdate.bind(this);
        this.onTripDelete = this.onTripDelete.bind(this);

        this.state = {
            employees: [],
            loading: true,
            failed: false,
            error: ''
        }
    }

    componentDidMount() {
        this.populateTripsData();
    }

    onTripUpdate(id) {
        const { history } = this.props;
        history.push('/update/' + id);
    }

    onTripDelete(id) {
        const { history } = this.props;
        history.push('/delete/' + id);
    }

    populateTripsData() {
        axios.get("/Employee").then(result => {
            const response = result.data;
            this.setState({ employees: response, loading: false, failed: false, error: "" });
        }).catch(error => {
            this.setState({ employees: [], loading: false, failed: true, error: "Employees could not be loaded" });
        });
    }

    renderAllEmployeesTable(employees) {
        return (
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
                        employees.map(employee => (
                            <tr key={employee.employeeID}>
                                <td>{employee.employeeNumber}</td>
                                <td>{employee.firstName}</td>
                                <td>{employee.lastName}</td>
                                <td>{new Date(employee.dateJoined).toISOString().slice(0, 10)}</td>
                                <td>{employee.extension}</td>
                                <td>
                                    <div className="form-group">
                                        <button onClick={() => this.onTripUpdate(employee.employeeID)} className="btn btn-success">
                                            Update
                                    </button>
                                        <button onClick={() => this.onTripDelete(employee.employeeID)} className="btn btn-danger">
                                            Delete
                                    </button>
                                    </div>
                                </td>
                            </tr>
                        ))
                    }

                </tbody>
            </table>
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