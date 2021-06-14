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
import VkLoginCallback from "../pages/login/VkLoginCallback";
import {AnonymousRoute} from "../components/AnonymousRoute/AnonymousRoute";

export const App: FC = () => (
    <Router>
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
                        <Route exact path="/" component={LoginPage} />
                        <Route exact path="/registration" component={RegistrationPage} />
                        <Route exact path="/vk-login-callback" component={VkLoginCallback} />
                    </Switch>
                </AnonymousLayout>
            </AnonymousRoute>
            <Route path={["/vk-login-callback"]}>
                <Switch>
                    <Route exact path="/vk-login-callback:code" component={VkLoginCallback} />
                </Switch>
            </Route>
        </Switch>
    </Router>
);