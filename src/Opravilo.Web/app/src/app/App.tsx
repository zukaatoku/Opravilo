import "./style.scss";
import * as React from "react";
import {FC} from "react";
import AnonymousLayout  from "../layouts/AnonymousLayout/AnonymousLayout";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"
import LoginPage from "../pages/login/LoginPage";
import RegistrationPage from "../pages/registration/RegistrationPage";
import UserLayout from "../layouts/UserLayout/UserLayout";
import HomePage from "../pages/home/HomePage";
import {PrivateRoute} from "../components/PrivateRoute/PrivateRoute";

export interface HelloWorldProps {
    userName: string,
    lang: string
}

export const App: FC<HelloWorldProps> = (props: HelloWorldProps) => (
    <Router>
        <Switch>
            <Route path={["/home"]}>
                <UserLayout>
                    <Switch>
                        <PrivateRoute exact path="/home" component={HomePage}/>
                    </Switch>
                </UserLayout>
            </Route>
            <Route path={["/", "/registration"]}>
                <AnonymousLayout>
                    <Switch>
                        <Route exact path="/" component={LoginPage} />
                        <Route exact path="/registration" component={RegistrationPage} />
                    </Switch>
                </AnonymousLayout>
            </Route>
        </Switch>
    </Router>
);