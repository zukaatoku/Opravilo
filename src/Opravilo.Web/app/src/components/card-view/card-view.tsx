import React, {useEffect, useState} from 'react'
import {ICardModel, ICardViewProps} from './types'
import {Button, Form, Input, Space} from 'antd'
import {CheckOutlined, CloseOutlined, DeleteOutlined, EditOutlined} from '@ant-design/icons'

import './card-view.scss'

interface IEditModeProps extends ICardViewProps {
    onCancelClick: () => void
}

const EditMode = (props: IEditModeProps): JSX.Element => {
    const [form] = Form.useForm()
    const {card, onCancelClick, fetchingCard, onSaveClick} = props
    
    const {Item} = Form
    const {TextArea} = Input
    
    useEffect(() => {
        form.setFieldsValue(card)
    }, [form])
    
    const onSaveButtonClick = () => {
      form.submit()  
    }
    
    const onFinish = (values: ICardModel) => {
        onSaveClick({...values, id: card.id })
            .then(() => {
                onCancelClick()  
            })
    }
    
    return <div className="card-view">
        <Form form={form} onFinish={onFinish}>
            <header>
                <Item name="name">
                    <TextArea style={{width: '95%', fontSize: '1.5em', fontWeight: 500, padding: 0, marginBottom: '0.4em'}}  placeholder="Project Title"/>
                </Item>
            </header>
            <div className="body-wrapper">
                <div className="description-etc">
                    <Space direction="vertical" style={{width: '100%'}}>
                        <span><h4 className="header">Description</h4></span>
                        <Item name="description">
                            <TextArea style={{width: '100%', height: '300px', padding: 0}} />
                        </Item>
                    </Space>
                </div>
                <div className="buttons-panel">
                    <Space direction="vertical" style={{width: '100%'}} className="wrapper" size={0}>
                        <h4 className="header">Actions</h4>
                        <Button icon={<CheckOutlined />} type="text" block className="button" onClick={onSaveButtonClick} loading={fetchingCard}>Save</Button>
                        <Button icon={<CloseOutlined />} type="text" block className="button" onClick={onCancelClick}>Cancel</Button>
                        <Button icon={<DeleteOutlined />} type="text" block danger className="button">Remove</Button>
                    </Space>
                </div>
            </div>
        </Form>
    </div>
}

interface IReadModeProps extends ICardViewProps {
    onEditClick: () => void
}

const ReadMode = (props: IReadModeProps): JSX.Element => {
    const {card, onEditClick} = props
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
                    <Button icon={<EditOutlined />} type="text" block className="button" onClick={onEditClick}>Edit</Button>
                    <Button icon={<DeleteOutlined />} type="text" block danger className="button">Remove</Button>
                </Space>
            </div>
        </div>
    </div>
}

export const CardView = (props: ICardViewProps): JSX.Element => {
    const {card, fetchingCard, onSaveClick} = props
    const [editMode, setEditMode] = useState(false)
    
    // todo: поискать правило про style прям в компоненте
    // todo: transpileOnly в этом компоненте починило какую-то супер блядскую ошибку с выбиванием вебпак вотча
    // todo: https://www.npmjs.com/package/react-textarea-autosize
    
    return editMode 
        ? <EditMode card={card} onCancelClick={() => setEditMode(false)} fetchingCard={fetchingCard} onSaveClick={onSaveClick}/> 
        : <ReadMode card={card} onEditClick={() => setEditMode(true)}/>
}