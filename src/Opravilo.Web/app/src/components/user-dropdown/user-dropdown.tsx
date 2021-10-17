import {IUserDropdownProps} from "./types";
import {UserOutlined} from "@ant-design/icons";
import {Menu, Spin} from "antd";
import React, {useEffect} from "react";
import {useHistory} from "react-router-dom";

const { SubMenu } = Menu;

export const UserDropdown = (props: IUserDropdownProps): JSX.Element => {
    const history = useHistory();
    const {displayName, fetchDisplayName, fetchingDisplayName} = props
    
    useEffect(() => {
        if (!displayName) {
            fetchDisplayName()
        }  
    })
    
    const logout = async () => {
        props.onLogout();
        history.push("/");
    }
    return (
        <Spin spinning={fetchingDisplayName}>
            <Menu mode="horizontal" theme="dark">
                <SubMenu title={displayName} icon={<UserOutlined/>}>
                    <Menu.Item onClick={logout} danger>Logout</Menu.Item>
                </SubMenu>
            </Menu>
        </Spin>)
}