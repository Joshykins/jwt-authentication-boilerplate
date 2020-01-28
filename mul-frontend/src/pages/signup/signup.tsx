import React from 'react';
import './signup.scss';

export interface SignupProps { }

const Signup: React.FunctionComponent<SignupProps> = () => {
  return (
    <section className="panel">
      <h1>
        Registration
      </h1>
      <form>
        <label className="textLabel">Account Name</label>
        <input type="text" name="account" id="" placeholder="MyAccount" />
        <label className="textLabel">First Name</label>
        <input type="text" name="first name" id="" placeholder="Joe" />
        <label className="textLabel">Last Name</label>
        <input type="text" name="last name" id="" placeholder="Doe" />
        <label className="textLabel">Email</label>
        <input type="text" name="email" id="" placeholder="joedoe@email.com" />
        <label className="textLabel">Password</label>
        <input type="text" name="password" id="" placeholder="*********" />
        <label className="textLabel">Retype Password</label><label className="textError">&nbsp;&nbsp;&nbsp;*Passwords to not match</label>
        <input type="password" name="Retype Password" id="" placeholder="*********" />
        <button type="submit">
          Register
        </button>
      </form>
    </section>
  );
};

export default Signup;
