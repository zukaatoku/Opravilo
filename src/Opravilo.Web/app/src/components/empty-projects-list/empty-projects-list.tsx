import React from "react"
import {Button, Empty} from "antd";

export const EmptyProjectsList = (): JSX.Element => {
    return <Empty
        description={
            <span>
        No projects found
      </span>
        }
    >
        <Button type="primary">Create Now</Button>
    </Empty>
}