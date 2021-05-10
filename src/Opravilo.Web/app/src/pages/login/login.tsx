import * as React from "react";
import {FC, FunctionComponent} from "react";
import { Form, Input, Button } from "antd";

const layout = {
    labelCol: {
        span: 4,
    },
    wrapperCol: {
        span: 16
    },
}

const tailLayout = {
    wrapperCol: { offset: 4, span: 16 },
};

export const LoginPage: FC = () => (
  <Form {...layout}>
      <Form.Item label="Username" name="username" rules={[{required: true, message: "Please input your username!"}]}>
          <Input />
      </Form.Item>
      <Form.Item label="Password" name="password" rules={[{required: true, message: "Please input your password!"}]}>
          <Input.Password />
      </Form.Item>
      <Form.Item {...tailLayout}>
          <Button type="primary" htmlType="submit">
              Submit
          </Button>
      </Form.Item>
  </Form>
);