const AuthManager = () => {
    
    const setTokens = function (jwt: string, refresh: string) : boolean {
        window.localStorage.setItem("jwt", jwt);
        window.localStorage.setItem("refresh", refresh);
        return true;
    };
    
    const removeTokens = function() {
        window.localStorage.removeItem("jwt");
        window.localStorage.removeItem("refresh");
        return true;
    };
    
    const getJwtToken = function() : string {
        return window.localStorage.getItem("jwt");
    }
    
    return {
        setTokens,
        removeTokens,
        getJwtToken
    }
};

export default AuthManager();