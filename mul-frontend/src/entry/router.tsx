import * as React from 'react';
import { BrowserRouter, Route, Link, Switch } from 'react-router-dom';
import Home from '../pages/home/home';
import { Navigation } from '../components/navigation/Navigation';

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
              <Route component={NotFound} exact />
            </Switch>
        </BrowserRouter>
      </React.Suspense>
    </>
  );
};
