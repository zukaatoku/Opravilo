import * as React from "react";
import {FC} from "react";
import { useLocation } from "react-router-dom";

const VkLoginCallback: FC = () => {
    const location = useLocation();
    const query = new URLSearchParams(location.search);
    const code = query.get("code");
    console.log("code from component:");
    console.log(code);
    return <h1>Lol</h1>
};

export default VkLoginCallback;