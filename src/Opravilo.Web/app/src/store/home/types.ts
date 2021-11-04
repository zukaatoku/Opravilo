export interface IHomeState {
    fetchingProjects: boolean
    projects: IProjectModel[]
    
    createProjectsModalVisible: boolean
    fetchingCreateOrEditProject: boolean
    
    editingProject?: IProjectModel
}

export interface IProjectModel {
    id?: number
    name?: string
    description?: string
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