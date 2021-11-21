import {IFullStateModel} from "../../store/project/types";

export interface IColumnStateViewProps {
    states: IFullStateModel[]
    
    onRemove: (id: number) => void
    onAddStateClick: () => void
    onEditStateClick: (stateId: number) => void
}