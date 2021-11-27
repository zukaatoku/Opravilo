import React from 'react'
import {Button, Space} from 'antd'
import {DeleteOutlined, EditOutlined} from '@ant-design/icons'
import {IReadModeProps} from './types'

export const ReadMode = (props: IReadModeProps): JSX.Element => {
    const {card, onEditClick, onRemove, fetchingCard} = props

    return <div className="card-view">
        <header>
            <h2 style={{width: '95%'}}>{card.name}</h2>
        </header>
        <div className="body-wrapper">
            <div className="description-etc">
                <Space direction="vertical" style={{width: '100%'}}>
                    <span><h4 className="header">Description</h4></span>
                    <span>{card.description}</span>
                </Space>
            </div>
            <div className="buttons-panel">
                <Space direction="vertical" style={{width: '100%'}} className="wrapper" size={0}>
                    <h4 className="header">Actions</h4>
                    <Button icon={<EditOutlined />} type="text" block className="button" onClick={onEditClick} loading={fetchingCard}>Edit</Button>
                    <Button icon={<DeleteOutlined />} type="text" block danger className="button" onClick={onRemove} loading={fetchingCard}>Remove</Button>
                </Space>
            </div>
        </div>
    </div>
}