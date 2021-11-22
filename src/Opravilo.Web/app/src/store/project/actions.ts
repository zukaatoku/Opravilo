import {createAction} from '@reduxjs/toolkit'

export const hideStateModal = createAction('hideStateModal')
export const showStateModal = createAction('showStateModal')
export const showEditStateModal = createAction<number>('showEditStateModal')
export const showCardViewModal = createAction<number>('showCardViewModal')
export const closeCardViewModal = createAction('closeCardViewModal')