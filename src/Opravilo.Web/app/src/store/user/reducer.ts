import {IUserState} from "./types";
import {createReducer} from "@reduxjs/toolkit";
import {fetchUserInfo, onLogout} from "./thunks";
import Cookies from "js-cookie";

const initialState: IUserState = {
    displayName: null,
    fetching: false
}

export const userReducer = createReducer(initialState, (builder) => {
    builder.addCase(fetchUserInfo.fulfilled, (state, {payload}) => {
        return {...state, displayName: payload, fetching: false}
    }) 
    builder.addCase(fetchUserInfo.pending, (state) => {
        return {...state, fetching: true}
    }) 
    builder.addCase(fetchUserInfo.rejected, (state) => {
        return {...state, fetching: false}
    })
    builder.addCase(onLogout.pending, (state) => {
        Cookies.remove("X-AUTH-STATE")
        return {...state}
    })
})