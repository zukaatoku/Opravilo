import {IFullProjectModel} from "../../store/home/types";
import {RouteComponentProps} from "react-router-dom";

interface RouteParams {
    id: string
}

export interface IProjectBoardPageProps extends RouteComponentProps<RouteParams> {
    fetchingProject: boolean
    fetchProject: (id: number) => void
    currentProject?: IFullProjectModel
    
    removeState: (stateId: number) => void
}