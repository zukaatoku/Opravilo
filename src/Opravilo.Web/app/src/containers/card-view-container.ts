import {CardView, ICardViewProps} from '../components/card-view'
import {AppDispatch, AppState} from '../store/store'
import {connect} from 'react-redux'
import {selectedCardSelector} from '../store/selectors'

const mapStateToProps = (state: AppState) : Partial<ICardViewProps> => ({
    card: selectedCardSelector(state)
})

const mapDispatchToProps = (dispatch: AppDispatch) : Partial<ICardViewProps> => ({
})

export const CardViewContainer = connect(mapStateToProps, mapDispatchToProps)(CardView)