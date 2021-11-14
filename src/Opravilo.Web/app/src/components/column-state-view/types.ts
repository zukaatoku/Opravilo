import {IStateModel} from "../../store/home/types";

export interface IColumnStateViewProps {
    states: IStateModel[]
    
    onRemove: (id: number) => void
    onAddStateClick: () => void
    onEditStateClick: (stateId: number) => void
}