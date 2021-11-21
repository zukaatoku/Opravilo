import {ICardModel} from "../../../store/project/types";

export interface IStateColumnProps {
    id: number
    name: string
    
    cards?: ICardModel[]
    
    onRemove: (id: number) => void
    onEdit: (id: number) => void
}

export interface IColumnHeaderProps {
    id: number
    name: string
    onRemove: (id: number) => void
    onEdit: (id: number) => void
}

export interface IColumnBodyProps {
    cards?: ICardModel[]
}

export interface ICardPreviewProps {
    name: string
}

export interface IContextMenuProps {
    id: number
    onRemove: (id: number) => void
    onEdit: (id: number) => void
}

export interface IAddStateColumnProps {
    onClick: () => void
}