export interface IHomeState {
    fetchingProjects: boolean
    projects: IProjectModel[]
}

export interface IProjectModel {
    id?: number
    name?: string
    description?: string
}