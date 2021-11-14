import {IEditStateArgs, IStateModel} from "../../store/home/types";

export interface ICreateStateFormProps {
    onCancel: () => void
    fetching: boolean
    
    onOk: (name: string) => void
    onOkEdit: (args: IEditStateArgs) => void
    
    editingState?: IStateModel
}

export interface IFormProperties {
    name: string
}