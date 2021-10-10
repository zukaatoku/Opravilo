const AuthManager = () => {
    
    const authenticated = function() : boolean {
      return window.localStorage.getItem("authenticated") === "true";  
    };
    
    const authenticate = function() : void {
        window.localStorage.setItem("authenticated", "true");
    }
    
    const deauthenticate = function() : void {
        window.localStorage.setItem("authenticated", undefined);
    }
    
    return {
        authenticate,
        deauthenticate,
        authenticated
    }
};

export default AuthManager();