export interface IUserState {
    displayName?: string
    
    fetching: boolean
    errors?: string[]
}

export interface ITryLoginArgs {
    login: string
    password: string
}