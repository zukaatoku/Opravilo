import {CardView, ICardViewProps} from '../components/card-view'
import {AppState} from '../store/store'
import {connect} from 'react-redux'
import {selectedCardSelector} from '../store/selectors'

const mapStateToProps = (state: AppState) : Partial<ICardViewProps> => ({
    card: selectedCardSelector(state)
})

export const CardViewContainer = connect(mapStateToProps)(CardView)