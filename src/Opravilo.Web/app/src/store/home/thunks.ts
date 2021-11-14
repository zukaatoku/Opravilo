import {getClient} from "../../api/BaseClient";
import {createAsyncThunk} from "@reduxjs/toolkit";
import {ICreateProjectArgs, IEditProjectArgs, IEditStateArgs} from "./types";
import {CreateProjectRequest, CreateStateRequest, UpdateProjectRequest, UpdateStateRequest} from "../../api/client";
import {AppState} from "../store";

const client = getClient()

export const fetchProjects = createAsyncThunk(
    'fetchProjects', 
    async () => {
        return await client.projectsAll()
    }
)

export const fetchProject = createAsyncThunk(
    'fetchProject',
    async (projectId: number) => {
        return await client.projects2(projectId);
    }
)

export const createProject = createAsyncThunk(
    'createProject',
    async (args: ICreateProjectArgs, {dispatch}) => {
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

export const addState = createAsyncThunk(
    'addState',
    async (name: string, {dispatch, getState}) => {
        const appState = getState() as AppState
        const projectId = appState.home.currentProject.id;
        
        const request: CreateStateRequest = new CreateStateRequest({
            name: name
        })
        
        await client.states(projectId, request);
        dispatch(fetchProject(projectId))
    }
)

export const editState = createAsyncThunk(
    'editState',
    async (args: IEditStateArgs, {dispatch, getState}) => {
        const appState = getState() as AppState
        const projectId = appState.home.currentProject.id;

        const request: UpdateStateRequest = new UpdateStateRequest({
            name: args.name
        })

        await client.states2(projectId, args.stateId, request);
        dispatch(fetchProject(projectId))
    }
)
