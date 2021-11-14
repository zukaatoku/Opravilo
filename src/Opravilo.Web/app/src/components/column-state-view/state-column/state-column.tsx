import React from "react"
import {IColumnHeaderProps, IContextMenuProps, IStateColumnProps} from "./types";
import {Dropdown, Empty, Menu, Popconfirm} from "antd";
import {EllipsisOutlined, EditOutlined, DeleteOutlined} from "@ant-design/icons";

import "./state-column.scss"

const menu = (props: IContextMenuProps) => {
    return <Menu>
        <Menu.Item key="1" icon={<EditOutlined/>} onClick={() => props.onEdit(props.id)}>
            Edit
        </Menu.Item>
        <Menu.Item key="2" icon={<DeleteOutlined/>} danger>
            <Popconfirm title="Are you sure?" icon={<DeleteOutlined style={{color: "red"}}/>} okText="Yes"
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

export const StateColumn = (props: IStateColumnProps): JSX.Element => {
    const {name, id, onRemove, onEdit} = props
    
    return <div className="state-column">
        <ColumnHeader id={id} name={name} onRemove={onRemove} onEdit={onEdit} />
        <Empty />
    </div>
}