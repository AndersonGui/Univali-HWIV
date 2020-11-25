import React, { useState } from "react";
import { isAuthenticated, verificarToken } from "./auth";
import { Route, Switch, Redirect } from "react-router-dom"

import Login from "./components/Login";
import DashboardLayout from "./components/dashboard/DashboardLayout";

function Routes() {
    const [auth, setAuth] = useState(isAuthenticated());

    async function handleVerificarToken() {
        setAuth(await verificarToken());
    }

    handleVerificarToken();

    return (
        auth ?
            <Switch>
                <Route path="/admin" component={DashboardLayout} />
                <Redirect to="/admin" />
            </Switch> :
            <Switch>
                <Route path="/" exact component={Login} />
                <Redirect to="/" />
            </Switch>
    )
};

export default Routes;