export interface IHomeState {
    fetchingProjects: boolean
    projects: IProjectModel[]
    
    createProjectsModalVisible: boolean
    fetchingCreateOrEditProject: boolean
    
    editingProject?: IProjectModel
    
    fetchingCurrentProject: boolean
    currentProject?: IFullProjectModel
    
    createEditStateModalVisible: boolean
    fetchingCreateEditState: boolean
    editingState?: IStateModel
}

export interface IProjectModel {
    id?: number
    name?: string
    description?: string
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

export interface ICreateProjectArgs {
    name: string
    description: string
}

export interface IEditProjectArgs {
    id: number
    name: string
    description: string
}

export interface IEditStateArgs {
    stateId: number
    name: string
}