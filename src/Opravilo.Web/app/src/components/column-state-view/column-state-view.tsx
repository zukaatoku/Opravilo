import React from "react"
import {IColumnStateViewProps} from "./types";
import {StateColumn} from "./state-column";

import "./column-state-view.scss"
import {AddStateColumn} from "./state-column/add-state-column";
import {Space} from "antd";

export const ColumnStateView = (props: IColumnStateViewProps): JSX.Element => {
    
    const columns = props.states.map((s) => {
        return <StateColumn name={s.name} key={s.id} id={s.id} onRemove={props.onRemove} onEdit={props.onEditStateClick} cards={s.cards}/>
    })
    
    return <div className="column-state-view">
        <Space align="start">{columns}<AddStateColumn onClick={props.onAddStateClick}/></Space>
    </div>
}