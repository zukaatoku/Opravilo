import {FC} from "react";
import {Button, Form, Input} from "antd";
import * as React from "react";
import * as Api from "../../api/client";
import {Client, IRegistrationRequest, RegistrationRequest} from "../../api/client";

const Item = Form.Item;

interface FormProperties {
    username: string,
    displayName: string,
    password: string
}

const RegistrationForm: FC = () => {
    const [form] = Form.useForm();
    
    const onFinish = async (values: FormProperties) => {
      console.log(values);
      const client = new Client("https://localhost:5008");
      const request: RegistrationRequest = new RegistrationRequest({
          login: values.username,
          password: values.password,
          displayName: values.displayName
      });
       const result = await client.register(request);
       console.log(result);
    };

    return (<Form form={form} onFinish={onFinish}>
        <Item name="username" rules={[{required: true, message: "Please input your username!"}]}>
            <Input placeholder="Username"/>
        </Item>
        <Item name="displayName" rules={[{required: true, message: "Please input your display name!"}]}>
            <Input placeholder="Display Name"/>
        </Item>
        <Item name="password" rules={[{required: true, message: "Please input your password!"}]}>
            <Input.Password placeholder="Password"/>
        </Item>
        <Item name="confirm" dependencies={["password"]} hasFeedback
              rules={[{required: true, message: "Please input your password!"},
                  ({getFieldValue}) => ({
                      validator(_, value) {
                          if (!value || getFieldValue("password") === value) {
                              return Promise.resolve();
                          }
                          return Promise.reject(new Error("The two password that you entered do not match!"));
                      }
                  })]}>
            <Input.Password placeholder="Confirm Password"/>
        </Item>
        <Item>
            <Button type="primary" htmlType="submit" block>
                Register
            </Button>
        </Item>
    </Form>)
};

export default RegistrationForm;