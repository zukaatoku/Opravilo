import {getClient} from '../../api/BaseClient'
import {createAsyncThunk} from '@reduxjs/toolkit'
import {AppState} from '../store'
import {CreateCardRequest, CreateStateRequest, UpdateCardRequest, UpdateStateRequest} from '../../api/client'
import {ICardModel, IEditStateArgs} from './types'

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

export const editCard = createAsyncThunk(
    'editCard',
    async (args: ICardModel, {getState}) => {
        const appState = getState() as AppState
        const projectId = appState.project.currentProject.id
        
        const request: UpdateCardRequest = new UpdateCardRequest({
            name: args.name,
            description: args.description
        })
        
        return await client.cards2(args.id, projectId, request)
    }
)

export const createCard = createAsyncThunk(
    'createCard',
    async (args: ICardModel, {getState}) => {
        const appState = getState() as AppState
        const projectId = appState.project.currentProject.id
        const stateId = appState.project.selectedStateId

        const request: CreateCardRequest = new CreateCardRequest({
            name: args.name,
            description: args.description
        })

        return await client.cards(stateId, projectId, request)
    }
)

export const removeCard = createAsyncThunk(
    'removeCard',
    async (cardId: number, {getState}) => {
        // todo: test delay
        const appState = getState() as AppState
        const projectId = appState.project.currentProject.id
        
        await client.cards3(projectId, cardId)
        return cardId
    }
)