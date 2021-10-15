import {Alert, Button, Divider, Form, Input} from "antd";
import React, {useState} from "react";
import {VkLogo} from "../vk-logo";
import {LoginRequest} from "../../api/client";
import {useHistory} from "react-router-dom";
import OauthPopup from "react-oauth-popup";
import {getClient} from "../../api/BaseClient";
import {FormProperties} from "./types";
import "./login-form.scss"

const Item = Form.Item;

export const LoginForm = (): JSX.Element => {
    const history = useHistory();
    const [showError, setShowError] = useState(false);
    const [error, setError] = useState("");
    
    const onFinish = (values: FormProperties) => {
        const client = getClient();
        const request: LoginRequest = new LoginRequest({
            login: values.username,
            password: values.password
        });
        client
            .login(request)
            .then((res) => {
                if (!res.isSuccess) {
                    setShowError(true);
                    setError(res.errors[0]);
                }
                else {
                    setShowError(false);
                    history.push("/home");
                }
            });
    };
    
    const authUrl = "https://oauth.vk.com/authorize?client_id=7841557&redirect_uri=https://localhost:5011/vk-login-callback";
    
    const onClose = () => {
      console.log("modal closed");  
    };
    
    // todo: move to new component
    const onCode = (code: string, params: URLSearchParams) => {
        const client = getClient();
        client
            .loginVK(code)
            .then((res) => {
                if (res.isSuccess) {
                    history.push({
                        pathname: "/home"
                    });
                }
            });
    }
    
    return (<Form onFinish={onFinish} className="login-form">
        <Item name="username" rules={[{required: true, message: "Please input your username!"}]}>
            <Input placeholder="Username"/>
        </Item>
        <Item name="password" rules={[{required: true, message: "Please input your password!"}]}>
            <Input.Password placeholder="Password"/>
        </Item>
        <Item>
            <Button type="primary" htmlType="submit" block>
                Submit
            </Button>
        </Item>
        {
            showError
                ? <Alert message={error} type="error" />
                : <></>
        }
        <Divider>OR</Divider>
        <OauthPopup url={authUrl} title="Vk auth" onCode={onCode} onClose={onClose} width={780} height={500}>
            <div className="social-icons">
                <VkLogo/>
            </div>
        </OauthPopup>
    </Form>)
};