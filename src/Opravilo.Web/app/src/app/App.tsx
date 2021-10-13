import "./style.scss";
import React from "react";
import {AnonymousLayout}  from "../layouts/AnonymousLayout";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"
import {LoginPage} from "../pages/login";
import {RegistrationPage} from "../pages/registration";
import {UserLayout} from "../layouts/UserLayout";
import {HomePage} from "../pages/home";
import {PrivateRoute} from "../components/PrivateRoute";
import {VkLoginCallback} from "../pages/login";
import {AnonymousRoute} from "../components/AnonymousRoute";

export const App = (): JSX.Element => {
    return <Router>
        <Switch>
            <PrivateRoute path={["/home"]}>
                <UserLayout>
                    <Switch>
                        <Route exact path="/home" component={HomePage}/>
                    </Switch>
                </UserLayout>
            </PrivateRoute>
            <AnonymousRoute path={["/", "/registration"]} redirectPath="/home">
                <AnonymousLayout>
                    <Switch>
                        <Route exact path="/" component={LoginPage}/>
                        <Route exact path="/registration" component={RegistrationPage}/>
                        <Route exact path="/vk-login-callback" component={VkLoginCallback}/>
                    </Switch>
                </AnonymousLayout>
            </AnonymousRoute>
            <Route path={["/vk-login-callback"]}>
                <Switch>
                    <Route exact path="/vk-login-callback:code" component={VkLoginCallback}/>
                </Switch>
            </Route>
        </Switch>
    </Router>
};