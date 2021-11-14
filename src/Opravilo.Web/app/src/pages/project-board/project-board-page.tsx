import React, {useEffect} from "react"
import {IProjectBoardPageProps} from "./types";
import {Empty, Spin} from "antd";
import {Link, withRouter} from "react-router-dom";
import {ColumnStateView} from "../../components/column-state-view";

import "./project-board-page.scss"
import {CreateStateForm} from "../../components/create-state-form";

export const ProjectBoardPage = withRouter((props: IProjectBoardPageProps): JSX.Element => {
    const {
        fetchingProject,
        currentProject,
        fetchProject,
        removeState,
        createEditStateVisible,
        fetchingCreateEditState,
        hideStateModal,
        onAddState,
        onAddClick,
        editingState,
        onOkEdit,
        onEditClick
    } = props;
    const id = Number(props.match.params.id);

    useEffect(() => {
        fetchProject(id);
    }, [fetchProject])

    const toShow = currentProject
        ? (
            <>
                <Link to="/home">Back to Home</Link>
                <h1>{currentProject.name}</h1>
                <ColumnStateView states={currentProject.states} onRemove={removeState} onAddStateClick={onAddClick} onEditStateClick={onEditClick}/>
            </>
        )
        : <Empty/>

    return <div className="project-board-page">
        <Spin spinning={fetchingProject}>{toShow}</Spin>
        {createEditStateVisible &&
        <CreateStateForm onCancel={hideStateModal} fetching={fetchingCreateEditState} onOk={onAddState} editingState={editingState} onOkEdit={onOkEdit}/>}
    </div>
});