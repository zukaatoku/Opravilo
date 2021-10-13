import React, {useState} from "react";
import {Empty} from "antd";
import {getClient} from "../../api/BaseClient";
import {Button} from "antd";

export const HomePage = (): JSX.Element => {
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