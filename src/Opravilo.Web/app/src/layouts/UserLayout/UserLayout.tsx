import {FunctionComponent} from "react";
import {Layout} from "antd";
import Logo from "../../assets/logo.svg";
const { Header, Content } = Layout;
import * as React from "react";
import AuthManager from "../../auth/AuthManager";
import CustomFooter from "../../components/CustomFooter/CustomFooter";

const UserLayout: FunctionComponent = (props) => {
    const userName = AuthManager.getDisplayName();

    return (<Layout style={{height: "100vh"}}>
        <Header>
            <a href="/home">
                <img src={Logo} alt="Logo" className="headerLogo"/>
                <span className="headerText">OPRAVILO</span>
            </a>
            <div style={{float: "right", color:"white"}}>{userName}</div>
        </Header>
        <Content style={{padding: 24, margin: 0, minHeight: 280}}>
            {props.children}
        </Content>
        <CustomFooter />
    </Layout>)
}

export default UserLayout;