import {createAsyncThunk} from "@reduxjs/toolkit";
import {getClient} from "../../api/BaseClient";

const client = getClient()

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