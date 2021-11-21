export interface IProjectState {
    fetchingCurrentProject: boolean
    currentProject?: IFullProjectModel

    createEditStateModalVisible: boolean
    fetchingCreateEditState: boolean
    
    selectedStateId?: number
}

export interface IFullProjectModel {
    id?: number
    name?: string
    description?: string
    states?: IFullStateModel[]
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

export interface IFullStateModel {
    id?: number
    name?: string
    
    cards?: ICardModel[]
}

export interface ICardModel {
    id?: number
    name?: string
    description?: string
}

export interface IEditStateArgs {
    stateId: number
    name: string
}