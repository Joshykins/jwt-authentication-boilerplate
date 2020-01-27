import * as React from 'react';
import { Link } from 'react-router-dom';
import './404.scss';


const NotFound: React.FunctionComponent = () => {
  return (
    <div className="notFound">
      <h1>404, Page not found!</h1>
      <Link to="/">Home</Link>
    </div>
  );
};

export default NotFound;
