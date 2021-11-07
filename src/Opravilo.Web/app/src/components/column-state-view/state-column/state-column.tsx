import React from "react"
import {IStateColumnProps} from "./types";
import {Dropdown, Empty, Menu} from "antd";
import {EllipsisOutlined} from "@ant-design/icons";

import "./state-column.scss"

const menu = (
    <Menu>
        <Menu.Item key="1">
            1st menu item
        </Menu.Item>
        <Menu.Item key="2">
            2nd menu item
        </Menu.Item>
        <Menu.Item key="3">
            3rd menu item
        </Menu.Item>
    </Menu>
);

const ColumnHeader = (name: string): JSX.Element => {
    return <header>
            <h2>{name}</h2>
            <Dropdown overlay={menu}>
                <EllipsisOutlined className="dots" />
            </Dropdown>
        </header>
}

export const StateColumn = (props: IStateColumnProps): JSX.Element => {
    const {name} = props
    
    return <div className="state-column">
        {ColumnHeader(name)}
        <Empty />
    </div>
}