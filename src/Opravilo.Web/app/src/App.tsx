import * as React from "react";
import {FC} from "react";
import { Layout } from "antd";
import { LoginPage } from "./pages/login/login";
import "antd/dist/antd.css";

const { Header, Footer, Sider, Content } = Layout;

export interface HelloWorldProps {
    userName: string,
    lang: string
}

export const App: FC<HelloWorldProps> = (props: HelloWorldProps) => (
    <Layout style={{height: "100vh"}}>
        <Header>Header</Header>
        <Content style={{padding: 24, margin: 0, minHeight: 280 }} >
            <LoginPage /> 
        </Content>
        <Footer>Footer</Footer>
    </Layout>
);