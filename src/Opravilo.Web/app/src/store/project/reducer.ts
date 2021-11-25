import {IProjectState} from './types'
import {createReducer} from '@reduxjs/toolkit'
import {addState, editCard, editState, fetchProject, removeState} from './thunks'
import {
    closeCardViewModal,
    hideStateModal,
    showCardViewModal,
    showEditStateModal,
    showStateModal,
} from './actions'

const initialState: IProjectState = {
    fetchingCurrentProject: false,
    createEditStateModalVisible: false,
    fetchingCreateEditState: false,
    cardViewModalVisible: false,
    fetchingCard: false
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
        return {...state, createEditStateModalVisible: false, selectedStateId: undefined}
    })
    builder.addCase(showStateModal, (state) => {
        return {...state, createEditStateModalVisible: true}
    })
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
        return {...state, fetchingCreateEditState: false, createEditStateModalVisible: false, selectedStateId: undefined}
    })
    builder.addCase(editState.rejected, (state) => {
        return {...state, fetchingCreateEditState: false, selectedStateId: undefined}
    })
    builder.addCase(showEditStateModal, (state, {payload}) => {
        return {...state, createEditStateModalVisible: true, selectedStateId: payload}
    })
    builder.addCase(showCardViewModal, (state, {payload}) => {
        return {...state, selectedCardId: payload, cardViewModalVisible: true}
    })
    builder.addCase(closeCardViewModal, (state) => {
        return {...state, selectedCardId: undefined, cardViewModalVisible: false}
    })
    builder.addCase(editCard.pending, (state) => {
        return {...state, fetchingCard: true}
    })
    builder.addCase(editCard.fulfilled, (state, {payload}) => {
        /* eslint-disable */
        // @ts-ignore
        // todo: !!! РАЗОБРАТЬСЯ С ОБЯЗАТЕЛЬНОСТЬЮ ТИПОВ СРОЧНО!
        const id = payload.id
        let  {states} = state.currentProject

        states = [...states]

        const stateColumnIndex = states.findIndex(s => s.cards.some(c => c.id == id))
        const stateColumn = states[stateColumnIndex]
        const stateCards = [...stateColumn.cards]
        const index = stateCards.findIndex(s => s.id == id)
        stateCards.splice(index, 1, payload)
        states.splice(stateColumnIndex, 1,{...stateColumn, cards: stateCards})

        return {...state, currentProject: {...state.currentProject, states: [...states]}, fetchingCard: false}
    })
    builder.addCase(editCard.rejected, (state) => {
        return {...state, fetchingCard: false }
    })
})