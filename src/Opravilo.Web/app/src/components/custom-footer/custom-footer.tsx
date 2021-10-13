import React from "react";
import {Layout} from "antd";
import {GithubLogo} from "../github-logo";
const { Footer } = Layout;

export const CustomFooter = (): JSX.Element => (
    <Footer>
        <GithubLogo prompt="Project repository" url="https://github.com/ejenin/Opravilo"
                    cssClass="hoverableTransition"/>
    </Footer>
);