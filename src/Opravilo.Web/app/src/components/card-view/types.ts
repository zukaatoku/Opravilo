export interface ICardViewProps {
    card: ICardModel
    
    fetchingCard: boolean
    // todo: !!! разобраться с any
    onSaveClick: (changedCard: ICardModel) => Promise<any>
    onAddClick: (newCard: ICardModel) => void
    onRemoveClick: (cardId: number) => void
}

export interface ICardModel {
    id: number
    name: string
    description: string
}

export interface IReadModeProps {
    card: ICardModel
    fetchingCard: boolean
    onEditClick: () => void
    onRemove: () => void
}

export interface IEditModeProps {
    card: ICardModel
    fetchingCard: boolean
    onCancelClick: () => void
    onRemove: () => void
    onSaveClick: (changedCard: ICardModel) => Promise<any>
    onAddClick: (newCard: ICardModel) => void
}
