import React from 'react'
import {IColumnStateViewProps} from './types'
import {StateColumn} from './state-column'
import {AddStateButton} from './state-column/add-state-button'
import {Space} from 'antd'

import './column-state-view.scss'

export const ColumnStateView = (props: IColumnStateViewProps): JSX.Element => {

    const columns = props.states.map((s) => {
        return <StateColumn name={s.name} key={s.id} id={s.id} onRemove={props.onRemove} onEdit={props.onEditStateClick}
                            cards={s.cards} onViewCardClick={props.onViewCardClick}/>
    })

    return <div className="column-state-view">
        <Space align="start">
            {columns}
            <AddStateButton onClick={props.onAddStateClick}/>
        </Space>
    </div>
}