import {CreateStateForm, ICreateStateFormProps} from "../../components/create-state-form";
import {AppDispatch, AppState} from "../../store/store";
import {connect} from "react-redux";
import {hideStateModal} from "../../store/home/actions";
import {addState, editState} from "../../store/home/thunks";
import {IEditStateArgs} from "../../store/home/types";

const mapStateToProps = (state: AppState) : Partial<ICreateStateFormProps> => ({
    fetching: state.home.fetchingCreateEditState,
    editingState: state.home.editingState,
})

const mapDispatchToProps = (dispatch : AppDispatch) : Partial<ICreateStateFormProps> => ({
    onCancel: () => dispatch(hideStateModal()),
    onOk: (name) => dispatch(addState(name)),
    onOkEdit: (args: IEditStateArgs) => dispatch(editState(args)),
})

export const CreateStateFormContainer = connect(mapStateToProps, mapDispatchToProps)(CreateStateForm)