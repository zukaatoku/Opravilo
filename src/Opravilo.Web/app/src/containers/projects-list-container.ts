import {AppDispatch, AppState} from "../store/store";
import {connect} from "react-redux";
import {IProjectsListProps, ProjectsList} from "../components/projects-list";

const mapStateToProps = (state: AppState) : Partial<IProjectsListProps> => ({
    projects: state.home.projects
});

const mapDispatchToProps = (dispatch: AppDispatch) : Partial<IProjectsListProps> => ({
})

export const ProjectsListContainer = connect(mapStateToProps, mapDispatchToProps)(ProjectsList)