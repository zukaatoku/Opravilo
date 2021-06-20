import * as React from "react";
import {FC, useState} from "react";
import {Empty} from "antd";
import {Client} from "../../api/client";

const HomePage: FC = () => {
    const client = new Client();
    const [name, setName] = useState("");
    client.displayName().then((res) => {
        setName(res);
    });
    
    return (<div>
        <span>{name}</span>
        <Empty />
    </div>)
};

export default HomePage;