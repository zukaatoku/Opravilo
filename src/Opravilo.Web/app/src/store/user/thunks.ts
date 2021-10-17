import {createAsyncThunk} from "@reduxjs/toolkit";
import {getClient} from "../../api/BaseClient";
import {ITryLoginArgs} from "./types";
import {LoginRequest} from "../../api/client";

const client = getClient()

export const tryLogin = createAsyncThunk(
    'tryLogin',
    async (credentials: ITryLoginArgs) => {
        const request: LoginRequest = new LoginRequest({
            login: credentials.login,
            password: credentials.password
        });
        return await client.login(request)
    }
)

export const tryVkLogin = createAsyncThunk(
    'tryVkLogin',
    async (code: string) => {
        return await client.loginVK(code)
    }
)

export const onLogout = createAsyncThunk(
    'onLogout',
    async () => {
        await client.logout()
    }
)

export const fetchUserInfo = createAsyncThunk(
    'fetchUserInfo',
    async () => {
        return await client.displayName()
    }
)