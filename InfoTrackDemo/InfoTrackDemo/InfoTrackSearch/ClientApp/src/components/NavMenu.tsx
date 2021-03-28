import React, { Component } from 'react';
// @ts-ignore
import { Container, Navbar, NavbarBrand, NavItem, NavLink} from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

interface Props {}
interface State {}

export class NavMenu extends Component<Props,State> {
  static displayName = NavMenu.name;

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">InfoTrackSearch</NavbarBrand>
            <ul className="navbar-nav flex-grow">
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
            </NavItem>
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/todo">ToDo</NavLink>
            </NavItem>
            </ul>
          </Container>
        </Navbar>
      </header>
    );
  }
}
