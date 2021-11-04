import {AppDispatch, AppState} from "../store/store";
import {HomePage, IHomePageProps} from "../pages/home";
import {connect} from "react-redux";
import {createProject, editProjectThunk, fetchProjects} from "../store/home/thunks";
import {hideCreateProjectModal, showCreateProjectModal} from "../store/home/actions";
import {ICreateProjectArgs, IEditProjectArgs} from "../store/home/types";

const mapStateToProps = (state: AppState) : Partial<IHomePageProps> => ({
    fetchingProjects: state.home.fetchingProjects,
    projectsEmpty: state.home.projects.length === 0,
    createProjectModalVisible: state.home.createProjectsModalVisible,
    fetchingCreateProject: state.home.fetchingCreateOrEditProject,
    editingProject: state.home.editingProject
})

const mapDispatchToProps = (dispatch: AppDispatch) : Partial<IHomePageProps> => ({
    fetchProjects: () => dispatch(fetchProjects()),
    showCreateProjectModal: () => dispatch(showCreateProjectModal()),
    hideCreateProjectModal: () => dispatch(hideCreateProjectModal()),
    onCreateProject: (args: ICreateProjectArgs) => dispatch(createProject(args)),
    onEditProject: (args: IEditProjectArgs) => dispatch(editProjectThunk(args))
})

export const HomePageContainer = connect(mapStateToProps, mapDispatchToProps)(HomePage)