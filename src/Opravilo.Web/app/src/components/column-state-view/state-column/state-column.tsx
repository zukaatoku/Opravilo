import React from 'react'
import {ICardPreviewProps, IColumnBodyProps, IColumnHeaderProps, IContextMenuProps, IStateColumnProps} from './types'
import {Button, Dropdown, Menu, Popconfirm, Space} from 'antd'
import {EllipsisOutlined, EditOutlined, DeleteOutlined, InfoCircleOutlined} from '@ant-design/icons'
import {AddCardButton} from './add-card-button'

import './state-column.scss'

const menu = (props: IContextMenuProps) => {
    return <Menu>
        <Menu.Item key="1" icon={<EditOutlined/>} onClick={() => props.onEdit(props.id)}>
            Edit
        </Menu.Item>
        <Menu.Item key="2" icon={<DeleteOutlined/>} danger>
            <Popconfirm title="Are you sure?" icon={<DeleteOutlined style={{color: 'red'}}/>} okText="Yes"
                        cancelText="No" onConfirm={() => props.onRemove(props.id)}>
                Remove
            </Popconfirm>
        </Menu.Item>
    </Menu>
}

const ColumnHeader = (props: IColumnHeaderProps): JSX.Element => {
    return <header>
            <h2>{props.name}</h2>
            <Dropdown overlay={menu({id: props.id, onRemove: props.onRemove, onEdit: props.onEdit})}>
                <EllipsisOutlined className="dots" />
            </Dropdown>
        </header>
}

const CardPreview = (props: ICardPreviewProps): JSX.Element => {
    const {id, name, onViewCardClick} = props
    
    return <div className="card-preview">
            <h3>{name}</h3>
            <Button icon={<InfoCircleOutlined />} type="text" onClick={() => onViewCardClick(id)}/>
        </div>
}

const ColumnBody = (props: IColumnBodyProps): JSX.Element => {
    const {cards, onViewCardClick, onAddCardClick, stateId} = props
    
    const cardsList = cards.map((c, i) => {
        return <CardPreview name={c.name} key={i} id={c.id} onViewCardClick={onViewCardClick}/>
    })
    
    return <div className="column-body">
        <Space direction="vertical" style={{width: '100%'}}>
            {cardsList}
            <AddCardButton onClick={() => onAddCardClick(stateId)}/>
        </Space>
    </div>
}

export const StateColumn = (props: IStateColumnProps): JSX.Element => {
    const {name, id, onRemove, onEdit, cards, onViewCardClick, onAddCardClick} = props
    
    return <div className="state-column">
        <ColumnHeader id={id} name={name} onRemove={onRemove} onEdit={onEdit} />
        <ColumnBody cards={cards} onViewCardClick={onViewCardClick} onAddCardClick={onAddCardClick} stateId={id}/>
    </div>
}