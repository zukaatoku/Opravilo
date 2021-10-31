import {IHomeState} from "./types";
import {createReducer} from "@reduxjs/toolkit";
import {fetchProjects} from "./thunks";

const initialState: IHomeState = {
    fetchingProjects: false,
    projects: []
}

export const homeReducer = createReducer(initialState, (builder) => {
    builder.addCase(fetchProjects.pending, (state) => {
      return {...state, fetchingProjects: true}  
    })
    builder.addCase(fetchProjects.fulfilled, (state, {payload}) => {
        return {...state, fetchingProjects: false, projects: payload}
    })
    builder.addCase(fetchProjects.rejected, (state) => {
        return {...state, fetchingProjects: false}
    })
})