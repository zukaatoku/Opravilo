import React from "react"
import "./state-column.scss"
import {PlusOutlined} from "@ant-design/icons";
import {Space} from "antd";

export const AddStateColumn = (): JSX.Element => {
    return <div className="state-column">
        <Space>
            <PlusOutlined /> Add state
        </Space>
    </div>
}