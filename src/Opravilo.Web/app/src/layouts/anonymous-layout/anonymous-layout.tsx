import "./anonymous-layout.scss";
import {Layout} from "antd";
import React from "react";
import Logo from "../../assets/logo.svg";
import {CustomFooter} from "../../components/custom-footer";

const { Header, Content } = Layout;

export const AnonymousLayout = (props: any): JSX.Element => (
    <Layout style={{height: "100vh"}}>
        <Header>
            <a href="/home">
                <img src={Logo} alt="Logo" className="headerLogo"/>
                <span className="headerText">OPRAVILO</span>
            </a>
        </Header>
        <Content style={{padding: 24, margin: 0, minHeight: 280 }} >
            {props.children}
        </Content>
        <CustomFooter />
    </Layout>
);