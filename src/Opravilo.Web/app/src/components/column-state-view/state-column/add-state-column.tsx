import React from "react"
import "./state-column.scss"
import {PlusOutlined} from "@ant-design/icons";
import {Button, Space} from "antd";

export const AddStateColumn = (): JSX.Element => {
    return <Button icon={<PlusOutlined/>} ghost className="add-state-button" type="dashed">Add state</Button>
}