import {IProjectState} from "./types";
import {createReducer} from "@reduxjs/toolkit";
import {addState, editState, fetchProject, removeState} from "./thunks";
import {hideStateModal, showEditStateModal, showStateModal} from "./actions";

const initialState: IProjectState = {
    fetchingCurrentProject: false,
    createEditStateModalVisible: false,
    fetchingCreateEditState: false
}

export const projectReducer = createReducer(initialState, (builder) => {
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