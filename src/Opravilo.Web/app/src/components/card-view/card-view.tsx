import React from 'react'
import {ICardViewProps} from './types'
import {Button, Space} from 'antd'
import {DeleteOutlined, EditOutlined} from '@ant-design/icons'

import './card-view.scss'

// todo: поискать правило про style прям в компоненте

export const CardView = (props: ICardViewProps): JSX.Element => {
    const {card} = props
    return <div className="card-view">
        <header><h2>{card.name}</h2></header>
        <div className="body-wrapper">
            <div className="description-etc">
                <Space direction="vertical">
                    <span><h4 className="header">Description</h4></span>
                    <span>{card.description}</span>
                </Space>
            </div>
            <div className="buttons-panel">
                <Space direction="vertical" style={{width: '100%'}} className="wrapper" size={0}>
                    <h4 className="header">Actions</h4>
                    <Button icon={<EditOutlined />} type="text" block className="button">Edit</Button>
                    <Button icon={<DeleteOutlined />} type="text" block danger className="button">Remove</Button>
                </Space>
            </div>
        </div>
    </div>
}