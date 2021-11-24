import React, {useState} from 'react'
import {ICardViewProps} from './types'
import {Button, Input, Space} from 'antd'
import {CheckOutlined, CloseOutlined, DeleteOutlined, EditOutlined} from '@ant-design/icons'

import './card-view.scss'

export const CardView = (props: ICardViewProps): JSX.Element => {
    const {card} = props
    const [editMode, setEditMode] = useState(false)
    
    // todo: поискать правило про style прям в компоненте
    // todo: transpileOnly в этом компоненте починило какую-то супер блядскую ошибку с выбиванием вебпак вотча
    // todo: https://www.npmjs.com/package/react-textarea-autosize
    
    return <div className="card-view">
        <header>
            {
                editMode ? <Input.TextArea defaultValue={card.name} style={{width: '95%', fontSize: '1.5em', fontWeight: 500, padding: 0, marginBottom: '0.4em'}}  placeholder="Project Title"/> : <h2 style={{width: '95%'}}>{card.name}</h2>
            }
        </header>
        <div className="body-wrapper">
            <div className="description-etc">
                <Space direction="vertical" style={{width: '100%'}}>
                    <span><h4 className="header">Description</h4></span>
                    {editMode ? <Input.TextArea defaultValue={card.description} style={{width: '100%', height: '300px', padding: 0}} />  : <span>{card.description}</span>}
                </Space>
            </div>
            <div className="buttons-panel">
                <Space direction="vertical" style={{width: '100%'}} className="wrapper" size={0}>
                    <h4 className="header">Actions</h4>
                    { editMode && <Button icon={<CheckOutlined />} type="text" block className="button">Save</Button>}
                    { 
                        editMode ? <Button icon={<CloseOutlined />} type="text" block className="button" onClick={() => setEditMode(!editMode)}>Cancel</Button> 
                            : <Button icon={<EditOutlined />} type="text" block className="button" onClick={() => setEditMode(!editMode)}>Edit</Button>
                    }
                    <Button icon={<DeleteOutlined />} type="text" block danger className="button">Remove</Button>
                </Space>
            </div>
        </div>
    </div>
}