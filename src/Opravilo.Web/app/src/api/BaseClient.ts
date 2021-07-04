import axios, {AxiosRequestConfig} from "axios";
import AuthManager from "../auth/AuthManager";
import {Client} from "./client";

export class BaseClient {
    constructor() {
        console.log("constructor");
    }
    
    protected transformOptions(options: AxiosRequestConfig) {
        // const token = AuthManager.getJwtToken();
        // if (token) {
        //     options.headers["Authorization"] = "Bearer " + token;
        // }
        return Promise.resolve(options);
    }
}

async function tryRefreshToken(): Promise<boolean> {
    console.log("refreshing token...");
    const client = new Client();
    const jwtToken = AuthManager.getJwtToken();
    const refreshToken = AuthManager.getRefreshToken();
    if (refreshToken) {
        const res = await client.refresh(jwtToken, refreshToken);
        if (res.isSuccess) {
            AuthManager.setTokens(res.token, res.refreshToken);
            return true;
        }
    }
    return false;
}

export function getClient(): Client {
    // todo: inherit "BaseClient" from generated client
    // todo: npm packet axios-auth-refresh
    const client = axios.create();

    client.interceptors.request.use((config) => {
        const token = AuthManager.getJwtToken();
        config.headers["Authorization"] = "Bearer " + token;
        return config;
    });
    
    client.interceptors.response.use((success) => {
        return success;
    }, async (err) => {
            const original = err.config;
            if (err.response.status === 401 && !err.config.__isRetryRequest) {
                const success = await tryRefreshToken();

                if (!success) {
                    AuthManager.removeTokens();
                    window.location.href = "/";
                }
                else {
                    const token = AuthManager.getJwtToken();
                    original.headers["Authorization"] = "Bearer " + token;
                    return axios.request(original);
                }
            }
    });
    return new Client(undefined, client);
}