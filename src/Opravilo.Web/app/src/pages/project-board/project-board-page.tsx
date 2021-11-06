import React, {useEffect} from "react"
import {IProjectBoardPageProps} from "./types";
import {Empty, Spin} from "antd";
import {Link, withRouter} from "react-router-dom";

export const ProjectBoardPage = withRouter((props: IProjectBoardPageProps): JSX.Element => {
    const {fetchingProject, currentProject, fetchProject} = props;
    const id = Number(props.match.params.id);
    
    useEffect(() => {
        fetchProject(id);
    }, [fetchProject])
    
    const toShow = currentProject 
        ? (
            <>
                <Link to="/home">Back to Home</Link>
                <h1>{currentProject.name}</h1>
            </>
        )
        : <Empty />
    
    return <Spin spinning={fetchingProject}>{toShow}</Spin>
});