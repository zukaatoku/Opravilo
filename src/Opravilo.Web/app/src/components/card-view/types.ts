export interface ICardViewProps {
    card: ICardModel
    
    fetchingCard?: boolean
    // todo: !!! разобраться с any
    onSaveClick?: (changedCard: ICardModel) => Promise<any>
}

export interface ICardModel {
    id: number
    name: string
    description: string
}