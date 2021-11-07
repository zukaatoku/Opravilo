import React from "react"
import {IStateColumnProps} from "./types";
import {Empty} from "antd";

import "./state-column.scss"

const ColumnHeader = (name: string): JSX.Element => {
    return <h2>{name}</h2>
}

export const StateColumn = (props: IStateColumnProps): JSX.Element => {
    return <div className="state-column">
        {ColumnHeader(props.name)}
        <Empty />
    </div>
}