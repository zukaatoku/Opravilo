import "./style.scss";
import {FunctionComponent} from "react";
import {Layout} from "antd";
import * as React from "react";
import GithubLogo from "../components/GithubLogo/GithubLogo";
const { Header, Footer, Content } = Layout;
import Logo from "../assets/logo.svg";

export const AnonymousLayout: FunctionComponent = (props) => (
    <Layout style={{height: "100vh"}}>
        <Header>
            <div className="headerText"><img src={Logo} alt="Logo"/> OPRAVILO</div> 
        </Header>
        <Content style={{padding: 24, margin: 0, minHeight: 280 }} >
            {props.children}
        </Content>
        <Footer>
            <GithubLogo prompt="Project repository" url="https://github.com/ejenin/Opravilo" cssClass="hoverableTransition"/>
        </Footer>
    </Layout>
);