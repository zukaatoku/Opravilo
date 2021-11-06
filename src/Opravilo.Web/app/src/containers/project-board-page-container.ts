import {IProjectBoardPageProps, ProjectBoardPage} from "../pages/project-board";
import {AppDispatch, AppState} from "../store/store";
import {connect} from "react-redux";
import {fetchProject} from "../store/home/thunks";

const mapStateToProps = (state: AppState) : Partial<IProjectBoardPageProps> => ({
    fetchingProject: state.home.fetchingCurrentProject,
    currentProject: state.home.currentProject,
})

const mapDispatchToProps = (dispatch: AppDispatch) : Partial<IProjectBoardPageProps> => ({
    fetchProject: (id) => dispatch(fetchProject(id))
})

export const ProjectBoardPageContainer = connect(mapStateToProps, mapDispatchToProps)(ProjectBoardPage)