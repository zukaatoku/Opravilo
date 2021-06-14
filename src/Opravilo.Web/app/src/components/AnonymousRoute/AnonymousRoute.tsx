import * as React from "react";
import {Redirect, Route, RouteProps} from "react-router-dom";
import {FC} from "react";
import AuthManager from "../../auth/AuthManager";

export interface AnonymousRouteProps extends RouteProps{
    redirectPath: string
}

export const AnonymousRoute: FC<AnonymousRouteProps> = (props: AnonymousRouteProps) => {
  const authenticated = AuthManager.authenticated(); 
  
  if (authenticated) {
      return <Redirect to={props.redirectPath} />;
  }
  
  return <Route {...props} />
};