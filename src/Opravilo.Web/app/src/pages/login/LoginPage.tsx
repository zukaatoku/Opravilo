import React from "react";
import { Col, Row } from "antd";
import {Card} from "antd";
import { Link } from "react-router-dom";
import {LoginForm} from "../../components/LoginForm";

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

export const LoginPage = (): JSX.Element => (
    <Row>
        <Col {...layout}>
            <Card title="Sign in" extra={<Link to="/registration">New user?</Link>}>
                <LoginForm />
            </Card>
        </Col>
    </Row>
);