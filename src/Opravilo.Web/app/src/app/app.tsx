import "./app.scss";
import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"
import {AnonymousLayout}  from "../layouts/anonymous-layout";
import {LoginPage} from "../pages/login";
import {RegistrationPage} from "../pages/registration";
import {UserLayout} from "../layouts/user-layout";
import {PrivateRoute} from "../components/private-route";
import {VkLoginCallback} from "../pages/login";
import {AnonymousRoute} from "../components/anonymous-route";
import {HomePageContainer} from "../containers/home-page-container";
import {ProjectBoardPage} from "../pages/project-board";

export const App = (): JSX.Element => {
    return <Router>
        <Switch>
            <PrivateRoute path={["/home", "/project"]}>
                <UserLayout>
                    <Switch>
                        <Route exact path="/home" component={HomePageContainer}/>
                        <Route exact path="/project/:id" component={ProjectBoardPage}/>
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