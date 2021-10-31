import {getClient} from "../../api/BaseClient";
import {createAsyncThunk} from "@reduxjs/toolkit";
import {delay} from "../../utils/delay";

const client = getClient()

export const fetchProjects = createAsyncThunk(
    'fetchProjects', 
    async () => {
        await delay(1000)
        return await client.projectsAll()
    }
)