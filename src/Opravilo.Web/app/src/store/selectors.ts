import {createSelector} from "@reduxjs/toolkit";
import {AppState} from "./store";
import {IStateModel} from "./project/types";
import {ISelectedState} from "../components/create-state-form/types";
import {IProjectModel} from "./home/types";
import {ISelectedProject} from "../components/create-project-form/types";

export const selectedStateSelector = createSelector(
    [(state: AppState) => state.project.currentProject.states, (state: AppState) => state.project.selectedStateId],
    (states: IStateModel[], stateId): ISelectedState => {
        return states.filter((s) => s.id === stateId)[0];
    }
)

export const selectedProjectSelector = createSelector(
    [(state: AppState) => state.home.projects, (state: AppState) => state.home.selectedProjectId],
    (projects: IProjectModel[], selectedProjectId): ISelectedProject => {
        return projects.filter((p) => p.id == selectedProjectId)[0]
    }
)