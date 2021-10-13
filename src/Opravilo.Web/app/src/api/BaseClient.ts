import axios, {AxiosRequestConfig} from "axios";
import AuthManager from "../auth/AuthManager";
import {Client} from "./client";

export class BaseClient {
    constructor() {
        console.log("constructor");
    }
    
    protected transformOptions(options: AxiosRequestConfig) {
        return Promise.resolve(options);
    }
}

async function tryRefreshToken(): Promise<boolean> {
    console.log("refreshing token...");
    const axiosClient = axios.create({withCredentials: true});
    const client = new Client(undefined, axiosClient);
    try {
        const res = await client.refresh();
        return res.isSuccess;
           
    } catch(e) {
        console.log(e);
        return false;
    }
}

export function getClient(): Client {
    // todo: inherit "BaseClient" from generated client
    // todo: npm packet axios-auth-refresh
    const client = axios.create({withCredentials: true});
    
    client.interceptors.response.use((success) => {
        return success;
    }, async (err) => {
            const original = err.config;
            if (err.response.status === 401 && !err.config.__isRetryRequest) {
                const success = await tryRefreshToken();

                if (!success) {
                    window.location.href = "/";
                }
                else {
                    return axios.request(original);
                }
            }
    });
    return new Client(undefined, client);
}