import {Button} from 'antd'
import {PlusOutlined} from '@ant-design/icons'
import React from 'react'

export const AddCardButton = (): JSX.Element => {
    return <Button icon={<PlusOutlined/>} ghost className="add-card-button" type="dashed">Add card</Button>
}