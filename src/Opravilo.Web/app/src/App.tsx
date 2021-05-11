import "antd/dist/antd.css";
import * as React from "react";
import {FC} from "react";
import { LoginPage } from "./pages/login/login";
import {AnonymousLayout} from "./layouts/AnonymousLayout";

export interface HelloWorldProps {
    userName: string,
    lang: string
}

export const App: FC<HelloWorldProps> = (props: HelloWorldProps) => (
    <AnonymousLayout>
        <LoginPage />
    </AnonymousLayout>
);