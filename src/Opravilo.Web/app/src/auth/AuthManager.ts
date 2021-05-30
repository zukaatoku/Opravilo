import jwt_decode from "jwt-decode";

interface JwtPayload {
    sub?: string;
    nickname: string;
}

const AuthManager = () => {
    let displayName: string = null;
    let userId: string = null;
    
    const setTokens = function (jwt: string, refresh: string) : boolean {
        window.localStorage.setItem("jwt", jwt);
        window.localStorage.setItem("refresh", refresh);
        
        const decoded = jwt_decode<JwtPayload>(jwt);
        userId = decoded.sub;
        displayName = decoded.nickname;
        
        console.log(displayName);
        
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