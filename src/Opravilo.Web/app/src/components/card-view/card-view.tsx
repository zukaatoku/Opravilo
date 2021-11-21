import React from "react"
import {ICardViewProps} from "./types";

import "./card-view.scss"
import {Button, Space} from "antd";
import {EditOutlined} from "@ant-design/icons";

export const CardView = (props: ICardViewProps): JSX.Element => {
    const {card} = props
    return <div className="card-view">
        <header><h2>{card.name}</h2></header>
        <div className="body-wrapper">
            <div className="description-etc">
                {card.description}
            </div>
            <div className="buttons-panel">
                <Space direction="vertical" style={{width: "100%"}} className="wrapper" size={0}>
                    <h4 className="header">Actions</h4>
                    <Button icon={<EditOutlined />} type="text" block className="button">Edit</Button>
                </Space>
            </div>
        </div>
    </div>
}