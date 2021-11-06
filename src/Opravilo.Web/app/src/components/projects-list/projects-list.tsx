import React from "react"
import {IProjectsListProps} from "./types";
import {Avatar, List, Typography} from "antd";
import {Link} from "react-router-dom"

import "./projects-list.scss"
import {useHistory} from "react-router-dom";

const { Title } = Typography

export const ProjectsList = (props: IProjectsListProps): JSX.Element => {
    const {editProject} = props;
    const history = useHistory();

    return <div className="projects-list">
        <Title level={4}>Projects list</Title>
        <List
            itemLayout="horizontal"
            dataSource={props.projects}
            bordered
            renderItem={item => (
                <List.Item
                    actions={[<a onClick={() => editProject(item.id)}>edit</a>,
                        <Link to={"/project/"+item.id}>view</Link>]}
                >
                    <List.Item.Meta
                        avatar={<Avatar src="https://joeschmoe.io/api/v1/random"/>}
                        title={item.name}
                        description={item.description}/>
                </List.Item>
            )}
        />
    </div>
}