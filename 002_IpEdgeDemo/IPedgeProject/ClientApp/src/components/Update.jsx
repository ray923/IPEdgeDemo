import React, { Component } from 'react';
import axios from 'axios';

export class Update extends Component {
    constructor(props) {
        super(props);

        this.onChangeEmployeeNumber = this.onChangeEmployeeNumber.bind(this);
        this.onChangeFirstName = this.onChangeFirstName.bind(this);
        this.onChangeLastName = this.onChangeLastName.bind(this);
        this.onChangeDateJoined = this.onChangeDateJoined.bind(this);
        this.onChangeExtension = this.onChangeExtension.bind(this);
        this.onChangeRoleID = this.onChangeRoleID.bind(this);
        this.onSubmit = this.onSubmit.bind(this);

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
                extension: response.extension,
                roleID: response.roleID
            })
        })
    }

    onChangeEmployeeNumber(e) {
        this.setState({
            employeeNumber: e.target.value
        });
    }

    onChangeFirstName(e) {
        this.setState({
            firstName: e.target.value
        });
    }

    onChangeLastName(e) {
        this.setState({
            lastName: e.target.value
        });
    }

    onChangeDateJoined(e) {
        this.setState({
            dateJoined: e.target.value
        });
    }

    onChangeExtension(e) {
        this.setState({
            extension: e.target.value
        });
    }

    onChangeRoleID(e) {
        this.setState({
            roleID: e.target.value
        });
    }

    onUpdateCancel() {
        const { history } = this.props;
        history.push('/');
    }

    onSubmit(e) {
        e.preventDefault();
        const { history } = this.props;
        const { id } = this.props.match.params;

        let employeeObject = {
            employeeNumber: this.state.employeeNumber,
            firstName: this.state.firstName,
            lastName: this.state.lastName,
            dateJoined: this.state.dateJoined,
            extension: this.state.extension,
            roleID: this.state.roleID,
        }

        axios.put("/Employee/UpdateEmployee/" + id, employeeObject).then(result => {
            history.push('/');
        })

    }

    render() {
        return (
            <div className="employee-form" >
                <h3>Update new Employee</h3>
                <form onSubmit={this.onSubmit}>
                    <div className="form-group">
                        <label>Employee Number:  </label>
                        <input
                            type="text"
                            className="form-control"
                            value={this.state.employeeNumber}
                            onChange={this.onChangeEmployeeNumber}
                        />
                    </div>
                    <div className="form-group">
                        <label>First Name:  </label>
                        <input
                            type="text"
                            className="form-control"
                            value={this.state.firstName}
                            onChange={this.onChangeFirstName}
                        />
                    </div>
                    <div className="form-group">
                        <label>Last Name:  </label>
                        <input
                            type="text"
                            className="form-control"
                            value={this.state.lastName}
                            onChange={this.onChangeLastName}
                        />
                    </div>
                    <div className="form-group">
                        <label>Extension: </label>
                        <input
                            type="text"
                            className="form-control"
                            value={this.state.extension}
                            onChange={this.onChangeExtension}
                        />
                    </div>
                    <div className="form-group">
                        <label>RoleID: </label>
                        <input
                            type="text"
                            className="form-control"
                            value={this.state.roleID}
                            onChange={this.onChangeRoleID}
                        />
                    </div>
                    <div className="row">
                        <div className="col col-md-6 col-sm-6 col-xs-12">
                            <div className="form-group">
                                <label>Date of joned:  </label>
                                <input
                                    type="date"
                                    className="form-control"
                                    value={this.state.dateJoined}
                                    onChange={this.onChangeDateJoined}
                                />
                            </div>
                        </div>
                    </div>


                    <div className="form-group">
                        <button onClick={this.onUpdateCancel} className="btn btn-default">Cancel</button>
                        <button type="submit" className="btn btn-success">Update</button>
                    </div>
                </form>
            </div>
        )
    }
}