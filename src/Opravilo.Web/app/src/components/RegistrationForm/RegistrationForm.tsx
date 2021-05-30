import {FC, useState} from "react";
import {Button, Form, Input, Alert, Spin} from "antd";
import * as React from "react";
import {Client, RegistrationRequest} from "../../api/client";
import AuthManager from "../../auth/AuthManager";

const Item = Form.Item;

interface FormProperties {
    username: string,
    displayName: string,
    password: string
}

const RegistrationForm: FC = () => {
    const [form] = Form.useForm();
    const [showError, setShowError] = useState(false);
    const [error, setError] = useState("");
    const [spinning, setSpinning] = useState(false);
    
    const onFinish = (values: FormProperties) => {
      setSpinning(true);
      const client = new Client();
      const request: RegistrationRequest = new RegistrationRequest({
          login: values.username,
          password: values.password,
          displayName: values.displayName
      });
      const result = client
           .register(request)
           .then((res) => {
           if (!res.isSuccess) {
               setShowError(true);
               setError(res.errors[0]);
           }
           else {
               setShowError(false);
               AuthManager.setTokens(res.token, res.refreshToken);
           }
           setSpinning(false);
      });
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
            <Spin spinning={spinning}>
                <Button type="primary" htmlType="submit" block>
                    Register
                </Button>
            </Spin>
        </Item>
        {
            showError
                ? <Alert message={error} type="error" />
                : <></>
        }
    </Form>)
};

export default RegistrationForm;