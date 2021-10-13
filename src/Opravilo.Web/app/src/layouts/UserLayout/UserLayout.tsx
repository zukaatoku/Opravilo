import {Layout} from "antd";
import Logo from "../../assets/logo.svg";
import React from "react";
import { Menu } from 'antd';
import {UserOutlined} from "@ant-design/icons";
import {useHistory} from "react-router-dom";
import {getClient} from "../../api/BaseClient";
import {CustomFooter} from "../../components/CustomFooter";

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