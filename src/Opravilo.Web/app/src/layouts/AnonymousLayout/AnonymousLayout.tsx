import "./style.scss";
import {FunctionComponent} from "react";
import {Layout} from "antd";
import * as React from "react";
const { Header, Content } = Layout;
import Logo from "../../assets/logo.svg";
import CustomFooter from "../../components/CustomFooter/CustomFooter";

const AnonymousLayout: FunctionComponent = (props) => (
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

export default AnonymousLayout;