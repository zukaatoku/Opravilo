export interface IStateColumnProps {
    id: number
    name: string
    
    onRemove: (id: number) => void
    onEdit: (id: number) => void
}

export interface IColumnHeaderProps {
    id: number
    name: string
    onRemove: (id: number) => void
    onEdit: (id: number) => void
}

export interface IContextMenuProps {
    id: number
    onRemove: (id: number) => void
    onEdit: (id: number) => void
}

export interface IAddStateColumnProps {
    onClick: () => void
}