import "./style.scss";
import * as React from "react";
import {FC} from "react";
import { AnonymousLayout } from "../layouts/AnonymousLayout";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"
import LoginPage from "../pages/login/LoginPage";
import RegistrationPage from "../pages/registration/RegistrationPage";

export interface HelloWorldProps {
    userName: string,
    lang: string
}

export const App: FC<HelloWorldProps> = (props: HelloWorldProps) => (
    <Router>
        <Switch>
            <AnonymousLayout>
                <Route exact path="/" component={LoginPage} />
                <Route exact path="/registration" component={RegistrationPage} />
            </AnonymousLayout>
        </Switch>
    </Router>
);