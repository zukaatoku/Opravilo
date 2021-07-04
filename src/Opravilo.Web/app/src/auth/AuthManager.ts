import jwt_decode from "jwt-decode";

interface JwtPayload {
    sub?: string;
    nickname: string;
}

const AuthManager = () => {
    
    const authenticated = function() : boolean {
      return window.localStorage.getItem("jwt") !== null;  
    };
    
    const setTokens = function (jwt: string, refresh: string) : boolean {
        window.localStorage.setItem("jwt", jwt);
        window.localStorage.setItem("refresh", refresh);
        
        const decoded = jwt_decode<JwtPayload>(jwt);

        window.localStorage.setItem("displayName", decoded.nickname);
        window.localStorage.setItem("userId", decoded.sub);
        
        return true;
    };
    
    const removeTokens = function() {
        window.localStorage.removeItem("jwt");
        window.localStorage.removeItem("refresh");
        window.localStorage.removeItem("displayName");
        window.localStorage.removeItem("userId");
        return true;
    };
    
    const getJwtToken = function() : string {
        return window.localStorage.getItem("jwt");
    };
    
    const getRefreshToken = function() : string {
        return window.localStorage.getItem("refresh");
    }
    
    const getDisplayName = function() : string {
        return window.localStorage.getItem("displayName");
    };
    
    return {
        setTokens,
        removeTokens,
        getJwtToken,
        getRefreshToken,
        getDisplayName,
        authenticated
    }
};

export default AuthManager();