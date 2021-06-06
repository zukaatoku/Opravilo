import * as React from "react";
import {FC} from "react";
import {Button} from "antd";
import AuthManager from "../../auth/AuthManager";
import {useHistory} from "react-router-dom";

const HomePage: FC = () => {
    const history = useHistory();
    const onLogout = () => {
        AuthManager.removeTokens();
        history.push("/");
    };
    
    return (<div>
        <h1> HOME SWEET HOME 2 </h1>
        <Button onClick={onLogout}>LOGOUT</Button>
    </div> )
};

export default HomePage;