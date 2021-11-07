import React from "react"
import {IStateColumnProps} from "./types";

import "./state-column.scss"

export const StateColumn = (props: IStateColumnProps): JSX.Element => {
    return <div className="state-column">
        <h1>{props.name}</h1>
        </div>
}