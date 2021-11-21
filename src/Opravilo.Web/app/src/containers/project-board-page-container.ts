import {IProjectBoardPageProps, ProjectBoardPage} from "../pages/project-board";
import {AppDispatch, AppState} from "../store/store";
import {connect} from "react-redux";
import {fetchProject, removeState} from "../store/project/thunks";
import {hideStateModal, showCardViewModal, showEditStateModal, showStateModal} from "../store/project/actions";

const mapStateToProps = (state: AppState) : Partial<IProjectBoardPageProps> => ({
    fetchingProject: state.project.fetchingCurrentProject,
    currentProject: state.project.currentProject,
    createEditStateVisible: state.project.createEditStateModalVisible
})

const mapDispatchToProps = (dispatch: AppDispatch) : Partial<IProjectBoardPageProps> => ({
    fetchProject: (id) => dispatch(fetchProject(id)),
    removeState: (stateId) => dispatch(removeState(stateId)),
    hideStateModal: () => dispatch(hideStateModal()),
    onAddClick: () => dispatch(showStateModal()),
    onEditClick: (stateId) => dispatch(showEditStateModal(stateId)),
    onViewCardClick: (cardId) => dispatch(showCardViewModal(cardId))
})

export const ProjectBoardPageContainer = connect(mapStateToProps, mapDispatchToProps)(ProjectBoardPage)