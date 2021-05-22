import {Button, Divider, Form, Input} from "antd";
import * as React from "react";
import {FC} from "react";
import VkLogo from "../VkLogo/VkLogo";

const Item = Form.Item;

const layout = {
    labelCol: { span: 8 },
    wrapperCol: { span: 16 },
};

const socialStyle = {
    textAlign: "center"
} as React.CSSProperties;

const LoginForm: FC = () => (
        <Form>
            <Item name="username" rules={[{required: true, message: "Please input your username!"}]}>
                <Input placeholder="Username"/>
            </Item>
            <Item name="password" rules={[{required: true, message: "Please input your password!"}]}>
                <Input.Password placeholder="Password" />
            </Item>
            <Item>
                <Button type="primary" htmlType="submit" block>
                    Submit
                </Button>
            </Item>
            <Divider>OR</Divider>
            <div style={socialStyle}>
                <VkLogo />
            </div>
        </Form>
);

export default LoginForm;