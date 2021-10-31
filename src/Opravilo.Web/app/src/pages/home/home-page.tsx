import React, {useEffect, useState} from "react";
import {EmptyProjectsList} from "../../components/empty-projects-list";
import {IHomePageProps} from "./types";
import {Button, Space, Spin} from "antd";
import {PlusOutlined, RedoOutlined} from "@ant-design/icons";
import {ProjectsListContainer} from "../../containers/projects-list-container";

import "./home-page.scss"

export const HomePage = (props: IHomePageProps): JSX.Element => {
    
    const {fetchProjects, fetchingProjects, projectsEmpty} = props;
    
    useEffect(() => {
       fetchProjects(); 
    }, [fetchProjects]);
    
    return (<div className="home-page">
        <Spin spinning={fetchingProjects}>
            <div className="buttons-panel">
                <Space className="space">
                    <Button type="primary" icon={<RedoOutlined />} onClick={fetchProjects}>Refresh</Button>
                    <Button type="primary" icon={<PlusOutlined />}>Create</Button>
                </Space>
            </div>
            {projectsEmpty ? <EmptyProjectsList /> : <ProjectsListContainer />}
        </Spin>
    </div>)
};