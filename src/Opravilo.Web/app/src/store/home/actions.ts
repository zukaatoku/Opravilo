import {createAction} from "@reduxjs/toolkit";

export const showCreateProjectModal = createAction("showCreateProjectModal")
export const hideCreateProjectModal = createAction("hideCreateProjectModal")
export const editProject = createAction<number>("editProject")
export const hideStateModal = createAction("hideStateModal")
export const showStateModal = createAction("showStateModal")
export const showEditStateModal = createAction<number>("showEditStateModal")