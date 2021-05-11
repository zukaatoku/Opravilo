import {FunctionComponent} from "react";
import {Layout} from "antd";
import * as React from "react";
const { Header, Footer, Content } = Layout;

export const AnonymousLayout: FunctionComponent = (props) => (
    <Layout style={{height: "100vh"}}>
        <Header>Header</Header>
        <Content style={{padding: 24, margin: 0, minHeight: 280 }} >
            {props.children}
        </Content>
        <Footer>Footer</Footer>
    </Layout>
);