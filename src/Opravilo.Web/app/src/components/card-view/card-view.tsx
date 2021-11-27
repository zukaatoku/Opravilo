import React, {useEffect, useState} from 'react'
import {ICardModel, ICardViewProps} from './types'
import {Button, Form, Input, Modal, Space} from 'antd'
import {CheckOutlined, CloseOutlined, DeleteOutlined, EditOutlined} from '@ant-design/icons'

import './card-view.scss'

interface IEditModeProps extends ICardViewProps {
    onCancelClick: () => void
    onRemove: () => void
}

const EditMode = (props: IEditModeProps): JSX.Element => {
    const [form] = Form.useForm()
    const {card, onCancelClick, fetchingCard, onSaveClick, onAddClick, onRemove} = props
    
    const {Item} = Form
    const {TextArea} = Input
    
    useEffect(() => {
        form.setFieldsValue(card)
    }, [form])
    
    const onSaveButtonClick = () => {
      form.submit()  
    }
    
    const onFinish = (values: ICardModel) => {
        const newCard = card != undefined ? {...values, id: card.id} : {...values}
        
        if (card) {
            onSaveClick(newCard)
                .then(() => {
                    onCancelClick()
                })   
        } else {
            onAddClick(newCard)
        }
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
                        { card && <Button icon={<CloseOutlined />} type="text" block className="button" onClick={onCancelClick} loading={fetchingCard}>Cancel</Button>}
                        { card && <Button icon={<DeleteOutlined />} type="text" block danger className="button" onClick={onRemove} loading={fetchingCard}>Remove</Button> }
                    </Space>
                </div>
            </div>
        </Form>
    </div>
}

interface IReadModeProps extends ICardViewProps {
    onEditClick: () => void
    onRemove: () => void
}

const ReadMode = (props: IReadModeProps): JSX.Element => {
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

export const CardView = (props: ICardViewProps): JSX.Element => {
    const {card, fetchingCard, onSaveClick, onRemoveClick, onAddClick} = props
    
    const initialState = card == undefined
    
    const [editMode, setEditMode] = useState(initialState)

    const onRemove = () => {
        Modal.confirm({
            title: 'Remove card',
            content: 'Are you sure you want to remove this card?',
            icon: <DeleteOutlined />,
            onOk: () => {
                onRemoveClick(card.id)
            }
        })
    }
    
    // todo: поискать правило про style прям в компоненте
    // todo: transpileOnly в этом компоненте починило какую-то супер блядскую ошибку с выбиванием вебпак вотча
    // todo: https://www.npmjs.com/package/react-textarea-autosize
    
    return editMode 
        ? <EditMode card={card} onCancelClick={() => setEditMode(false)} fetchingCard={fetchingCard} onSaveClick={onSaveClick} onRemove={onRemove} onAddClick={onAddClick}/> 
        : <ReadMode card={card} onEditClick={() => setEditMode(true)} onRemoveClick={onRemoveClick} fetchingCard={fetchingCard} onRemove={onRemove}/>
}