import axios from "axios";
import AuthManager from "../auth/AuthManager";

export class BaseClient {
    constructor() {
        axios.interceptors.request.use((config) => {
            const token = AuthManager.getJwtToken();
            console.log(token);
            return config;
        });
    }
    
    protected transformOptions(options: any) {
        const token = AuthManager.getJwtToken();
        if (token) {
            options.headers["Authorization"] = "Bearer " + token;
        }
        return Promise.resolve(options);
    }
}