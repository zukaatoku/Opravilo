import {IStateModel} from "../../store/project/types";

export interface IColumnStateViewProps {
    states: IStateModel[]
    
    onRemove: (id: number) => void
    onAddStateClick: () => void
    onEditStateClick: (stateId: number) => void
}