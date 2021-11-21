import React from "react"
import {ICardViewProps} from "./types";

export const CardView = (props: ICardViewProps): JSX.Element => {
    const {card} = props
    return <h1>{card.name}</h1>
}