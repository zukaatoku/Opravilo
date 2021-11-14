export interface IProjectState {
    fetchingCurrentProject: boolean
    currentProject?: IFullProjectModel

    createEditStateModalVisible: boolean
    fetchingCreateEditState: boolean
    editingState?: IStateModel
}

export interface IFullProjectModel {
    id?: number
    name?: string
    description?: string
    states?: IStateModel[]
    creator?: IUserModel
}

export interface IUserModel {
    id?: number
    displayName?: string
}

export interface IStateModel {
    id?: number
    name?: string
}

export interface IEditStateArgs {
    stateId: number
    name: string
}