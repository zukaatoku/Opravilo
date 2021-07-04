import * as React from "react";
import {FC, useState} from "react";
import {Empty} from "antd";
import {Client} from "../../api/client";
import {getClient} from "../../api/BaseClient";
import {Button} from "antd";

const HomePage: FC = () => {
    const [name, setName] = useState("");
    
    const onClick = () => {
        setName("loading...");
        const client = getClient();
        try {
            client.displayName()
                .then((res) => {
                    console.log(res);
                    setName(res);
                }).catch((err) => {
                console.log(err);
            });   
        }
        catch (e) {
            console.log(e);
        }
    };
    
    return (<div>
        <Button onClick={onClick}>Refresh</Button>
        <span>{name}</span>
        <Empty />
    </div>)
};

export default HomePage;