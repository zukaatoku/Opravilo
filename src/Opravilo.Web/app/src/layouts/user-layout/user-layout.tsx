import {Layout} from "antd";
import Logo from "../../assets/logo.svg";
import React from "react";
import {CustomFooter} from "../../components/custom-footer";
import "./user-layout.scss"
import { UserDropdownContainer } from "../../containers";

const { Header, Content } = Layout;

export const UserLayout = (props: any): JSX.Element => {
    // todo: avatar
    
    return (<Layout className="user-layout">
        <Header>
            <a href="/home">
                <img src={Logo} alt="Logo" className="header-logo"/>
                <span className="header-text">OPRAVILO</span>
            </a>
            <div className="header-menu">
                <UserDropdownContainer />
            </div>
        </Header>
        <Content className="content-body">
            {props.children}
        </Content>
        <CustomFooter />
    </Layout>)
}