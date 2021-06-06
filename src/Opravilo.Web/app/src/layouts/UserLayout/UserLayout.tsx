import {FunctionComponent} from "react";
import {Layout} from "antd";
import Logo from "../../assets/logo.svg";
import GithubLogo from "../../components/GithubLogo/GithubLogo";
const { Header, Footer, Content } = Layout;
import * as React from "react";
import AuthManager from "../../auth/AuthManager";

const UserLayout: FunctionComponent = (props) => {
    const userName = AuthManager.getDisplayName();

    return (<Layout style={{height: "100vh"}}>
        <Header>
            <div>
                <img src={Logo} alt="Logo" className="headerLogo"/>
                <span className="headerText">USER LAYOUT</span>
            </div>
            <div style={{float: "right"}}>{userName}</div>
        </Header>
        <Content style={{padding: 24, margin: 0, minHeight: 280}}>
            {props.children}
        </Content>
        <Footer>
            <GithubLogo prompt="Project repository" url="https://github.com/ejenin/Opravilo"
                        cssClass="hoverableTransition"/>
        </Footer>
    </Layout>)
}

export default UserLayout;