import {FC} from "react";
import GithubLogo from "../GithubLogo/GithubLogo";
import * as React from "react";
import {Layout} from "antd";
const { Footer } = Layout;

const CustomFooter: FC = () => (
    <Footer>
        <GithubLogo prompt="Project repository" url="https://github.com/ejenin/Opravilo"
                    cssClass="hoverableTransition"/>
    </Footer>
);

export default CustomFooter;