import React, { Component } from 'react';
import axios from 'axios';

export class Delete extends Component {
    constructor(props) {
        super(props);

        this.onCancel = this.onCancel.bind(this);
        this.onConfirmation = this.onConfirmation.bind(this);

        this.state = {
            employeeNumber: null,
            firstName: '',
            lastName: '',
            dateJoined: null,
            extension: null,
            roleID: null,
        }
    }

    componentDidMount() {
        const { id } = this.props.match.params;

        axios.get("/Employee/SingleEmployee/" + id).then(employee => {
            const response = employee.data;

            this.setState({
                employeeNumber: response.employeeNumber,
                firstName: response.firstName,
                lastName: response.lastName,
                dateJoined: new Date(response.dateJoined).toISOString().slice(0, 10),
            })
        })
    }

    onCancel(e) {
        const { history } = this.props;
        history.push('/');
    }

    onConfirmation(e) {
        const { id } = this.props.match.params;
        const { history } = this.props;

        axios.delete("/Employee/DeleteEmployee/" + id).then(result => {
            history.push('/');
        })
    }

    render() {
        return (
            <div style={{ marginTop: 10 }}>
                <h2>Delete employee confirmation</h2>
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">EmployeeNumber: {this.state.employeeNumber} </h4>
                        <p class="card-text">Name: {this.state.firstName}  {this.state.lastName}</p>
                        <p class="card-text">JoinedDate: {this.state.dateJoined}</p>
                        <button onClick={this.onCancel} class="btn btn-default">
                            Cancel
                    </button>
                        <button onClick={this.onConfirmation} class="btn btn-danger">
                            Confirm
                    </button>
                    </div>
                </div>
            </div>
        )
    }
}