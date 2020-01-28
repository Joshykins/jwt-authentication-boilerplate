import React from 'react';
import './login.scss';
import { Link } from 'react-router-dom';

export interface LoginProps { }

const Login: React.FunctionComponent<LoginProps> = () => {
  return (
    <section className="panel">
      <h1>
        Login
      </h1>
      <form>
        <label className="textLabel">Email</label>
        <input type="text" name="email" id="" placeholder="joedoe@email.com" />
        <label className="textLabel">Password</label>
        <input type="text" name="password" id="" placeholder="*********" />
        <button type="submit">
          Login
        </button>
        <Link to="/forgot">Forgot your password.</Link>
      </form>
    </section>
  );
};

export default Login;
