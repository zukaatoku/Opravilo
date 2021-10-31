import {getClient} from "../../api/BaseClient";
import {createAsyncThunk} from "@reduxjs/toolkit";
import {delay} from "../../utils/delay";
import {ICreateProjectArgs} from "./types";
import {CreateProjectRequest} from "../../api/client";

const client = getClient()

export const fetchProjects = createAsyncThunk(
    'fetchProjects', 
    async () => {
        await delay(1000)
        return await client.projectsAll()
    }
)

export const createProject = createAsyncThunk(
    'createProject',
    async (args: ICreateProjectArgs, {dispatch}) => {
        await delay(1000)
        const request: CreateProjectRequest = new CreateProjectRequest({
            description: args.description,
            name: args.name
        })
        
        await client.projects(request)
        dispatch(fetchProjects())
    }
)