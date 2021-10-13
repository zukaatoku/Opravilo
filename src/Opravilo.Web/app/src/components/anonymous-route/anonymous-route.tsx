import React from "react";
import {Redirect, Route} from "react-router-dom";
import AuthManager from "../../auth/AuthManager";
import {AnonymousRouteProps} from "./types";

export const AnonymousRoute = (props: AnonymousRouteProps): JSX.Element => {
  const authenticated = AuthManager.authenticated(); 
  
  if (authenticated) {
      return <Redirect to={props.redirectPath} />;
  }
  
  return <Route {...props} />
};