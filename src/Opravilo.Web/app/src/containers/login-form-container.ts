import {AppDispatch, AppState} from "../store/store";
import {connect} from "react-redux";
import {ILoginFormProps, LoginForm} from "../components/login-form";

const mapStateToProps = (state: AppState) : Partial<ILoginFormProps> => ({
});

const mapDispatchToProps = (dispatch: AppDispatch) => ({
})

export const LoginFormContainer = connect(mapStateToProps, mapDispatchToProps)(LoginForm)