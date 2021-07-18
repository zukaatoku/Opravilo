import * as React from "react";
import { FC } from "react";
import {Card, Col, Row} from "antd";
import RegistrationForm from "../../components/RegistrationForm/RegistrationForm";
import { Link } from "react-router-dom";

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

const RegistrationPage: FC = () => (
    <Row>
        <Col {...layout}>
            <Card title="Sign up" extra={<Link to="/">Back</Link>}>
                <RegistrationForm />
            </Card>
        </Col>
    </Row>
);

export default RegistrationPage;