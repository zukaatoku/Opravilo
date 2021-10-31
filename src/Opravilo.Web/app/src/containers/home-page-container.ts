import {AppDispatch, AppState} from "../store/store";
import {HomePage, IHomePageProps} from "../pages/home";
import {connect} from "react-redux";
import {fetchProjects} from "../store/home/thunks";

const mapStateToProps = (state: AppState) : Partial<IHomePageProps> => ({
    fetchingProjects: state.home.fetchingProjects,
    projectsEmpty: state.home.projects.length === 0
})

const mapDispatchToProps = (dispatch: AppDispatch) : Partial<IHomePageProps> => ({
    fetchProjects: () => dispatch(fetchProjects())
})

export const HomePageContainer = connect(mapStateToProps, mapDispatchToProps)(HomePage)