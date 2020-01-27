import React from 'react'
import { Link } from 'react-router-dom'
import './Navigation.scss';

export const Navigation: React.FunctionComponent = () => {
  return (
    <nav className="nav">
      <div className="navLeft">
        <Link className="navLink" to="/">Home</Link>
        <Link className="navLink" to="/421421dsa">404</Link>
      </div>
      <div className="navRight">
        <Link className="navLink" to="/signup">Sign-up</Link>
        
        <Link className="navLink" to="/signin">Login</Link>
      </div>
    </nav>
  )
};


