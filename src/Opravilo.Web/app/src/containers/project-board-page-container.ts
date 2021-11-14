import {IProjectBoardPageProps, ProjectBoardPage} from "../pages/project-board";
import {AppDispatch, AppState} from "../store/store";
import {connect} from "react-redux";
import {addState, editState, fetchProject, removeState} from "../store/home/thunks";
import {hideStateModal, showEditStateModal, showStateModal} from "../store/home/actions";
import {IEditStateArgs} from "../store/home/types";

const mapStateToProps = (state: AppState) : Partial<IProjectBoardPageProps> => ({
    fetchingProject: state.home.fetchingCurrentProject,
    currentProject: state.home.currentProject,
    createEditStateVisible: state.home.createEditStateModalVisible,
    fetchingCreateEditState: state.home.fetchingCreateEditState,
    editingState: state.home.editingState
})

const mapDispatchToProps = (dispatch: AppDispatch) : Partial<IProjectBoardPageProps> => ({
    fetchProject: (id) => dispatch(fetchProject(id)),
    removeState: (stateId) => dispatch(removeState(stateId)),
    hideStateModal: () => dispatch(hideStateModal()),
    onAddState: (name) => dispatch(addState(name)),
    onAddClick: () => dispatch(showStateModal()),
    onOkEdit: (args: IEditStateArgs) => dispatch(editState(args)),
    onEditClick: (stateId) => dispatch(showEditStateModal(stateId))
})

export const ProjectBoardPageContainer = connect(mapStateToProps, mapDispatchToProps)(ProjectBoardPage)