import {ICreateProjectArgs, IEditProjectArgs, IProjectModel} from "../../store/home/types";

export interface IHomePageProps {
    fetchingProjects: boolean
    fetchProjects: () => void
    projectsEmpty: boolean
    
    createProjectModalVisible: boolean
    showCreateProjectModal: () => void
    hideCreateProjectModal: () => void
    
    onCreateProject: (args: ICreateProjectArgs) => void
    fetchingCreateProject: boolean
    
    editingProject?: IProjectModel
    onEditProject: (args: IEditProjectArgs) => void
}