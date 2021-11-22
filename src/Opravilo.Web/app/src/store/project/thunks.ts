import {getClient} from '../../api/BaseClient'
import {createAsyncThunk} from '@reduxjs/toolkit'
import {AppState} from '../store'
import {CreateStateRequest, UpdateStateRequest} from '../../api/client'
import {IEditStateArgs} from './types'

const client = getClient()

export const fetchProject = createAsyncThunk(
    'fetchProject',
    async (projectId: number) => {
        return await client.projects2(projectId)
    }
)

export const removeState = createAsyncThunk(
    'removeState',
    async (stateId: number, {dispatch, getState}) => {
        const appState = getState() as AppState
        const projectId = appState.project.currentProject.id

        await client.states3(projectId, stateId)
        dispatch(fetchProject(projectId))
    }
)

export const addState = createAsyncThunk(
    'addState',
    async (name: string, {dispatch, getState}) => {
        const appState = getState() as AppState
        const projectId = appState.project.currentProject.id

        const request: CreateStateRequest = new CreateStateRequest({
            name: name
        })

        await client.states(projectId, request)
        dispatch(fetchProject(projectId))
    }
)

export const editState = createAsyncThunk(
    'editState',
    async (args: IEditStateArgs, {dispatch, getState}) => {
        const appState = getState() as AppState
        const projectId = appState.project.currentProject.id

        const request: UpdateStateRequest = new UpdateStateRequest({
            name: args.name
        })

        await client.states2(projectId, args.stateId, request)
        dispatch(fetchProject(projectId))
    }
)