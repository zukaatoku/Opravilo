import {Layout} from "antd";
import Logo from "../../assets/logo.svg";
import React from "react";
import { Menu } from 'antd';
import {UserOutlined} from "@ant-design/icons";
import {useHistory} from "react-router-dom";
import {getClient} from "../../api/BaseClient";
import {CustomFooter} from "../../components/custom-footer";
import "./user-layout.scss"

const { Header, Content } = Layout;
const { SubMenu } = Menu;

export const UserLayout = (props: any): JSX.Element => {
    const userName = "AuthManager.getDisplayName()";
    const history = useHistory();
    
    const onLogout = () => {
        const client = getClient();
        client.logout()
            .then(() => {
                history.push("/");  
            })
    };
    
    // todo: avatar
    
    return (<Layout className="user-layout">
        <Header>
            <a href="/home">
                <img src={Logo} alt="Logo" className="header-logo"/>
                <span className="header-text">OPRAVILO</span>
            </a>
            <Menu mode="horizontal" theme="dark" className="header-menu">
                <SubMenu title={userName} icon={<UserOutlined/>}>
                    <Menu.Item onClick={onLogout} danger>Logout</Menu.Item>
                </SubMenu>
            </Menu>
        </Header>
        <Content className="content-body">
            {props.children}
        </Content>
        <CustomFooter />
    </Layout>)
}