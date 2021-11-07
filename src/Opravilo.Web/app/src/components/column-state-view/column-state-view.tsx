import React from "react"
import {IColumnStateViewProps} from "./types";
import {StateColumn} from "./state-column";

import "./column-state-view.scss"

export const ColumnStateView = (props: IColumnStateViewProps): JSX.Element => {
    
    const columns = props.states.map((s) => {
        return <StateColumn name={s.name} key={s.id}/>
    })
    
    return <div className="column-state-view">
        {columns}
    </div>
}