import {FunctionComponent} from "react";
import {Layout} from "antd";
import Logo from "../../assets/logo.svg";
const { Header, Content } = Layout;
import * as React from "react";
import AuthManager from "../../auth/AuthManager";
import CustomFooter from "../../components/CustomFooter/CustomFooter";
import { Menu } from 'antd';
import {UserOutlined} from "@ant-design/icons";
import {useHistory} from "react-router-dom";
import {getClient} from "../../api/BaseClient";

const { SubMenu } = Menu;

const UserLayout: FunctionComponent = (props) => {
    const userName = AuthManager.getDisplayName();
    const history = useHistory();
    
    const onLogout = () => {
        // AuthManager.removeTokens();
        const client = getClient();
        // client.logout
        client.logout()
            .then(() => {
                history.push("/");  
            })
    };
    
    // todo: avatar
    
    return (<Layout style={{height: "100vh"}}>
        <Header>
            <a href="/home">
                <img src={Logo} alt="Logo" className="headerLogo"/>
                <span className="headerText">OPRAVILO</span>
            </a>
            <Menu mode="horizontal" theme="dark" style={{float: "right"}}>
                <SubMenu title={userName} icon={<UserOutlined/>}>
                    <Menu.Item onClick={onLogout} danger>Logout</Menu.Item>
                </SubMenu>
            </Menu>
        </Header>
        <Content style={{padding: 24, margin: 0, minHeight: 280}}>
            {props.children}
        </Content>
        <CustomFooter />
    </Layout>)
}

export default UserLayout;