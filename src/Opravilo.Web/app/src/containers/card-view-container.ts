import {CardView, ICardViewProps} from '../components/card-view'
import {AppDispatch, AppState} from '../store/store'
import {connect} from 'react-redux'
import {selectedCardSelector} from '../store/selectors'
import {createCard, editCard, removeCard} from '../store/project/thunks'

const mapStateToProps = (state: AppState) : Partial<ICardViewProps> => ({
    card: selectedCardSelector(state),
    fetchingCard: state.project.fetchingCard
})

const mapDispatchToProps = (dispatch: AppDispatch) : Partial<ICardViewProps> => ({
    // todo: split to 'add' and 'save'
    onSaveClick: (args) => args.id != undefined ?  dispatch(editCard(args)) : dispatch(createCard(args)),
    onRemoveClick: (cardId) => dispatch(removeCard(cardId))
})

export const CardViewContainer = connect(mapStateToProps, mapDispatchToProps)(CardView)