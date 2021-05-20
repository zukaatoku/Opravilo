import * as React from "react";
import { FC } from "react";
import { Form, Input, Button, Col, Row } from "antd";
import LoginForm from "../../components/LoginForm/LoginForm";
import {Card} from "antd";

const {Meta} = Card;

const layout = {
    lg: {
        span: 6,
        offset: 9   
    },
    md: {
        span: 8,
        offset: 8
    },
    sm: {
        span: 24,
    },
    xs: {
        span: 24
    }
};

const LoginPage: FC = () => (
    <Row>
        <Col {...layout}>
            <Card title="Sign in" extra={<a>New user?</a>}>
                <LoginForm />
            </Card>
        </Col>
    </Row>
);

export default LoginPage;