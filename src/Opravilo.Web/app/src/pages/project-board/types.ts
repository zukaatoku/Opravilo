import {IEditStateArgs, IFullProjectModel, IStateModel} from "../../store/home/types";
import {RouteComponentProps} from "react-router-dom";

interface RouteParams {
    id: string
}

export interface IProjectBoardPageProps extends RouteComponentProps<RouteParams> {
    fetchingProject: boolean
    fetchProject: (id: number) => void
    currentProject?: IFullProjectModel
    
    removeState: (stateId: number) => void
    
    createEditStateVisible: boolean
    fetchingCreateEditState: boolean
    hideStateModal: () => void
    
    onAddState: (name: string) => void
    onAddClick: () => void,
    
    editingState?: IStateModel
    onOkEdit: (args: IEditStateArgs) => void
    onEditClick: (stateId: number) => void
}