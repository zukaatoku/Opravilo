import {IProjectBoardPageProps, ProjectBoardPage} from "../pages/project-board";
import {AppDispatch, AppState} from "../store/store";
import {connect} from "react-redux";
import {addState, editState, fetchProject, removeState} from "../store/home/thunks";
import {hideStateModal, showEditStateModal, showStateModal} from "../store/home/actions";

const mapStateToProps = (state: AppState) : Partial<IProjectBoardPageProps> => ({
    fetchingProject: state.home.fetchingCurrentProject,
    currentProject: state.home.currentProject,
    createEditStateVisible: state.home.createEditStateModalVisible
})

const mapDispatchToProps = (dispatch: AppDispatch) : Partial<IProjectBoardPageProps> => ({
    fetchProject: (id) => dispatch(fetchProject(id)),
    removeState: (stateId) => dispatch(removeState(stateId)),
    hideStateModal: () => dispatch(hideStateModal()),
    onAddClick: () => dispatch(showStateModal()),
    onEditClick: (stateId) => dispatch(showEditStateModal(stateId))
})

export const ProjectBoardPageContainer = connect(mapStateToProps, mapDispatchToProps)(ProjectBoardPage)