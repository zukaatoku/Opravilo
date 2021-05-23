import {FC} from "react";
import {Button, Form, Input} from "antd";
import * as React from "react";

const Item = Form.Item;

const RegistrationForm: FC = () => (
  <Form>
      <Item name="username" rules={[{required: true, message: "Please input your username!"}]}>
          <Input placeholder="Username"/>
      </Item>
      <Item name="displayName" rules={[{required: true, message: "Please input your display name!"}]}>
          <Input placeholder="Display Name"/>
      </Item>
      <Item name="password" rules={[{required: true, message: "Please input your password!"}]}>
          <Input.Password placeholder="Password" />
      </Item>
      <Item>
          <Button type="primary" htmlType="submit" block>
              Register
          </Button>
      </Item>
  </Form>    
);

export default RegistrationForm;