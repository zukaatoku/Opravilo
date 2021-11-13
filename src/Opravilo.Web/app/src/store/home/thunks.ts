import {getClient} from "../../api/BaseClient";
import {createAsyncThunk} from "@reduxjs/toolkit";
import {delay} from "../../utils/delay";
import {ICreateProjectArgs, IEditProjectArgs} from "./types";
import {CreateProjectRequest, UpdateProjectRequest} from "../../api/client";
import {AppState} from "../store";

const client = getClient()

export const fetchProjects = createAsyncThunk(
    'fetchProjects', 
    async () => {
        // await delay(1000)
        return await client.projectsAll()
    }
)

export const fetchProject = createAsyncThunk(
    'fetchProject',
    async (projectId: number) => {
        // await delay(1000)
        return await client.projects2(projectId);
    }
)

export const createProject = createAsyncThunk(
    'createProject',
    async (args: ICreateProjectArgs, {dispatch}) => {
        // await delay(1000)
        const request: CreateProjectRequest = new CreateProjectRequest({
            description: args.description,
            name: args.name
        })
        
        await client.projects(request)
        dispatch(fetchProjects())
    }
)

export const editProjectThunk = createAsyncThunk(
    'editProject',
    async (args: IEditProjectArgs, {dispatch}) => {
        // await delay(1000)
        
        const request: UpdateProjectRequest = new UpdateProjectRequest({
            description: args.description,
            name: args.name,
        })

        await client.projects4(args.id, request)
        dispatch(fetchProjects())
    }
)

export const removeState = createAsyncThunk(
    'removeState',
    async (stateId: number, {dispatch, getState}) => {
        const appState = getState() as AppState
        const projectId = appState.home.currentProject.id;
        
        await client.states3(projectId, stateId);
        dispatch(fetchProject(projectId))
    }
)