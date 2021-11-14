import {IHomeState} from "./types";
import {createReducer} from "@reduxjs/toolkit";
import {addState, createProject, editProjectThunk, editState, fetchProject, fetchProjects, removeState} from "./thunks";
import {
    editProject,
    hideCreateProjectModal,
    hideStateModal,
    showCreateProjectModal,
    showEditStateModal,
    showStateModal
} from "./actions";

const initialState: IHomeState = {
    fetchingProjects: false,
    projects: [],
    createProjectsModalVisible: false,
    fetchingCreateOrEditProject: false,
    fetchingCurrentProject: false,
    createEditStateModalVisible: false,
    fetchingCreateEditState: false
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
        return {...state, createProjectsModalVisible: false, editingProject: undefined}
    })
    builder.addCase(createProject.pending, (state) => {
        return {...state, fetchingCreateOrEditProject: true}
    })
    builder.addCase(createProject.fulfilled, (state) => {
        return {...state, fetchingCreateOrEditProject: false, createProjectsModalVisible: false}
    })
    builder.addCase(createProject.rejected, (state) => {
        return {...state, fetchingCreateOrEditProject: false}
    })
    builder.addCase(editProject, (state, {payload}) => {
       const selectedProject = state.projects.filter(p => p.id == payload)[0];
       return {...state, createProjectsModalVisible: true, editingProject: selectedProject}
    });
    builder.addCase(editProjectThunk.pending, (state) => {
        return {...state, fetchingCreateOrEditProject: true}
    })
    builder.addCase(editProjectThunk.fulfilled, (state) => {
        return {...state, fetchingCreateOrEditProject: false, createProjectsModalVisible: false, editingProject: undefined}
    })
    builder.addCase(editProjectThunk.rejected, (state) => {
        return {...state, fetchingCreateOrEditProject: false, editingProject: undefined}
    })
    builder.addCase(fetchProject.pending, (state) => {
        return {...state, fetchingCurrentProject: true}
    })
    builder.addCase(fetchProject.fulfilled, (state, {payload}) => {
        return {...state, fetchingCurrentProject: false, currentProject: payload}
    })
    builder.addCase(fetchProject.rejected, (state) => {
        return {...state, fetchingCurrentProject: false}
    })
    builder.addCase(removeState.pending, (state) => {
        return {...state, fetchingCurrentProject: true}
    })
    builder.addCase(removeState.fulfilled, (state) => {
        return {...state, fetchingCurrentProject: false}
    })
    builder.addCase(removeState.rejected, (state) => {
        return {...state, fetchingCurrentProject: false}
    })
    builder.addCase(hideStateModal, (state) => {
       return {...state, createEditStateModalVisible: false, editingState: undefined} 
    });
    builder.addCase(showStateModal, (state) => {
        return {...state, createEditStateModalVisible: true}
    });

    builder.addCase(addState.pending, (state) => {
        return {...state, fetchingCreateEditState: true}
    })
    builder.addCase(addState.fulfilled, (state) => {
        return {...state, fetchingCreateEditState: false, createEditStateModalVisible: false}
    })
    builder.addCase(addState.rejected, (state) => {
        return {...state, fetchingCreateEditState: false}
    })
    builder.addCase(editState.pending, (state) => {
        return {...state, fetchingCreateEditState: true}
    })
    builder.addCase(editState.fulfilled, (state) => {
        return {...state, fetchingCreateEditState: false, createEditStateModalVisible: false, editingState: undefined}
    })
    builder.addCase(editState.rejected, (state) => {
        return {...state, fetchingCreateEditState: false, editingState: undefined}
    })
    builder.addCase(showEditStateModal, (state, {payload}) => {
        const selectedState = state.currentProject.states.filter(p => p.id == payload)[0];
        return {...state, createEditStateModalVisible: true, editingState: selectedState}
    });
})