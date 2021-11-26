export interface ICardViewProps {
    card: ICardModel
    
    fetchingCard?: boolean
    // todo: !!! разобраться с any
    onSaveClick?: (changedCard: ICardModel) => Promise<any>
    onAddClick?: (newCard: ICardModel) => void
    onRemoveClick: (cardId: number) => void
}

export interface ICardModel {
    id: number
    name: string
    description: string
}