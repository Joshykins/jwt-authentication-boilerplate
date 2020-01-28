import * as React from 'react';
import { BrowserRouter, Route, Link, Switch } from 'react-router-dom';
import Home from '../pages/home/home';
import { Navigation } from '../components/navigation/Navigation';
import Signup from '../pages/signup/signup';
import Login from '../pages/login/login';

const NotFound = React.lazy(() => import('../components/404/404'));

export const AppRouter = () => {
  return (
    <>
    
    <div className="coolBackground">
      </div>

      <React.Suspense fallback={<div>Loading...</div>}>
        
        <BrowserRouter>
            <Navigation />
            {/*Switch component renders first match on route, if not, it renders not found page.*/}
            <Switch>
              <Route path="/" exact component={Home} />
              <Route path="/signup" exact component={Signup} />
              <Route path="/signin" exact component={Login} />
              <Route component={NotFound} exact />
            </Switch>
        </BrowserRouter>
      </React.Suspense>
    </>
  );
};
