import {FunctionComponent} from "react";
import {Layout} from "antd";
import * as React from "react";
import GithubLogo from "../components/GithubLogo/GithubLogo";
const { Header, Footer, Content } = Layout;

export const AnonymousLayout: FunctionComponent = (props) => (
    <Layout style={{height: "100vh"}}>
        <Header>Header</Header>
        <Content style={{padding: 24, margin: 0, minHeight: 280 }} >
            {props.children}
        </Content>
        <Footer><GithubLogo prompt="Project repository" url="https://github.com/ejenin/Opravilo"/></Footer>
    </Layout>
);