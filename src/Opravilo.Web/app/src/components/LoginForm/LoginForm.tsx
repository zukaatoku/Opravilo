import {Alert, Button, Divider, Form, Input} from "antd";
import * as React from "react";
import {FC, useState} from "react";
import VkLogo from "../VkLogo/VkLogo";
import {Client, LoginRequest} from "../../api/client";
import AuthManager from "../../auth/AuthManager";

const Item = Form.Item;

const socialStyle = {
    textAlign: "center"
} as React.CSSProperties;

interface FormProperties {
    username: string,
    password: string
}

const LoginForm: FC = () => {

    const [showError, setShowError] = useState(false);
    const [error, setError] = useState("");
    
    const onFinish = (values: FormProperties) => {
        const client = new Client();
        const request: LoginRequest = new LoginRequest({
            login: values.username,
            password: values.password
        });
        const result = client
            .login2(request)
            .then((res) => {
                if (!res.isSuccess) {
                    setShowError(true);
                    setError(res.errors[0]);
                }
                else {
                    setShowError(false);
                    AuthManager.setTokens(res.token, res.refreshToken);
                }
            });
    };
    
    return (<Form onFinish={onFinish}>
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
        <div style={socialStyle}>
            <VkLogo/>
        </div>
    </Form>)
};

export default LoginForm;