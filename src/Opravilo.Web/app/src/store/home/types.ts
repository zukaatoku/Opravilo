export interface IHomeState {
    fetchingProjects: boolean
    projects: IProjectModel[]
    
    createProjectsModalVisible: boolean
    fetchingCreateProject: boolean
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