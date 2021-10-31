import React from "react";
import {Form, Input, Modal} from "antd";
import {ICreateProjectFormProps, IFormProperties} from "./types";

const {Item} = Form

const layout = {
    labelCol: { span: 5 },
    wrapperCol: { span: 19 },
};

export const CreateProjectForm = (props: ICreateProjectFormProps): JSX.Element => {
    const [form] = Form.useForm();
    const {onCancel, fetchingCreateProject, onCreate} = props

    const onOk = async () => {
        form.submit()
    }
    
    const onFinish = (values: IFormProperties) => {
        onCreate(values)
    }
    
    return <Modal visible={true} onCancel={onCancel} title="Create Project" onOk={onOk} confirmLoading={fetchingCreateProject}>
            <Form {...layout} form={form} onFinish={onFinish}>
                <Item label="Name" name="name" rules={[{required: true}]}>
                    <Input />
                </Item>
                <Item label="Description" name="description" rules={[{required: true}]}>
                    <Input.TextArea />
                </Item>
            </Form>
        </Modal>
}