import {configureStore, getDefaultMiddleware} from "@reduxjs/toolkit"
import { userReducer } from './user/reducer'
import {homeReducer} from "./home/reducer";

export const store = configureStore({
    reducer: {
        user: userReducer,
        home: homeReducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({serializableCheck: false})
})

export type AppState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch