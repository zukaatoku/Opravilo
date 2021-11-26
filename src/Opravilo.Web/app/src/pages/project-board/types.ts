import {RouteComponentProps} from 'react-router-dom'
import {IFullProjectModel} from '../../store/project/types'

interface RouteParams {
    id: string
}

export interface IProjectBoardPageProps extends RouteComponentProps<RouteParams> {
    fetchingProject: boolean
    fetchProject: (id: number) => void
    currentProject?: IFullProjectModel
    
    removeState: (stateId: number) => void
    
    createEditStateVisible: boolean
    hideStateModal: () => void
    
    onAddClick: () => void
    onEditClick: (stateId: number) => void

    onViewCardClick: (cardId: number) => void
    cardViewModalVisible: boolean
    onCloseCardViewModal: () => void
    
    onAddCardClick: (stateId: number) => void
}