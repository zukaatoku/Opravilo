import {ICreateProjectArgs} from "../../store/home/types";

export interface IFormProperties {
    name: string
    description: string
}

export interface ICreateProjectFormProps {
    onCancel: () => void
    onCreate: (args: ICreateProjectArgs) => void
    fetchingCreateProject: boolean
}