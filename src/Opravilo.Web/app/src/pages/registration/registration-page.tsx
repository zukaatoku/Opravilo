import React from "react";
import {Card, Col, Row} from "antd";
import { Link } from "react-router-dom";
import {RegistrationForm} from "../../components/registration-form";
import { RegistrationFormContainer } from "../../containers/registration-form-container";

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

export const RegistrationPage = (): JSX.Element => (
    <Row>
        <Col {...layout}>
            <Card title="Sign up" extra={<Link to="/">Back</Link>}>
                <RegistrationFormContainer />
            </Card>
        </Col>
    </Row>
);