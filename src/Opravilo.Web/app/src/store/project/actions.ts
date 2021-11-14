import {createAction} from "@reduxjs/toolkit";

export const hideStateModal = createAction("hideStateModal")
export const showStateModal = createAction("showStateModal")
export const showEditStateModal = createAction<number>("showEditStateModal")