import {IProjectBoardPageProps, ProjectBoardPage} from '../pages/project-board'
import {AppDispatch, AppState} from '../store/store'
import {connect} from 'react-redux'
import {changeState, fetchProject, removeState} from '../store/project/thunks'
import {
    addCardClick,
    closeCardViewModal,
    hideStateModal,
    showCardViewModal,
    showEditStateModal,
    showStateModal
} from '../store/project/actions'

const mapStateToProps = (state: AppState) : Partial<IProjectBoardPageProps> => ({
    fetchingProject: state.project.fetchingCurrentProject,
    currentProject: state.project.currentProject,
    createEditStateVisible: state.project.createEditStateModalVisible,
    cardViewModalVisible: state.project.cardViewModalVisible,
    fetchingChangeState: state.project.fetchingChangeState
})

const mapDispatchToProps = (dispatch: AppDispatch) : Partial<IProjectBoardPageProps> => ({
    fetchProject: (id) => dispatch(fetchProject(id)),
    removeState: (stateId) => dispatch(removeState(stateId)),
    hideStateModal: () => dispatch(hideStateModal()),
    onAddClick: () => dispatch(showStateModal()),
    onEditClick: (stateId) => dispatch(showEditStateModal(stateId)),
    onViewCardClick: (cardId) => dispatch(showCardViewModal(cardId)),
    onCloseCardViewModal: () => dispatch(closeCardViewModal()),
    onAddCardClick: (stateId) => dispatch(addCardClick(stateId)),
    onChangeState: (cardId, newStateId) => dispatch(changeState({cardId, newStateId}))
})

export const ProjectBoardPageContainer = connect(mapStateToProps, mapDispatchToProps)(ProjectBoardPage)