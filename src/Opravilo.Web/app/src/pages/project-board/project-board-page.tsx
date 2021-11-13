import React, {useEffect} from "react"
import {IProjectBoardPageProps} from "./types";
import {Empty, Spin} from "antd";
import {Link, withRouter} from "react-router-dom";
import {ColumnStateView} from "../../components/column-state-view";

import "./project-board-page.scss"

export const ProjectBoardPage = withRouter((props: IProjectBoardPageProps): JSX.Element => {
    const {fetchingProject, currentProject, fetchProject, removeState} = props;
    const id = Number(props.match.params.id);
    
    useEffect(() => {
        fetchProject(id);
    }, [fetchProject])
    
    const toShow = currentProject 
        ? (
            <>
                <Link to="/home">Back to Home</Link>
                <h1>{currentProject.name}</h1>
                <ColumnStateView states={currentProject.states} onRemove={removeState}/>
            </>
        )
        : <Empty />
    
    return <div className="project-board-page">
        <Spin spinning={fetchingProject}>{toShow}</Spin>
    </div>
});