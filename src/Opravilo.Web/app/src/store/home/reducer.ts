import {IHomeState} from "./types";
import {createReducer} from "@reduxjs/toolkit";
import {createProject, fetchProjects} from "./thunks";
import {hideCreateProjectModal, showCreateProjectModal} from "./actions";

const initialState: IHomeState = {
    fetchingProjects: false,
    projects: [],
    createProjectsModalVisible: false,
    fetchingCreateProject: false
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
    builder.addCase(showCreateProjectModal, (state) => {
        return {...state, createProjectsModalVisible: true}
    })
    builder.addCase(hideCreateProjectModal, (state) => {
        return {...state, createProjectsModalVisible: false}
    })
    builder.addCase(createProject.pending, (state) => {
        return {...state, fetchingCreateProject: true}
    })
    builder.addCase(createProject.fulfilled, (state) => {
        return {...state, fetchingCreateProject: false, createProjectsModalVisible: false}
    })
    builder.addCase(createProject.rejected, (state) => {
        return {...state, fetchingCreateProject: false}
    })
})