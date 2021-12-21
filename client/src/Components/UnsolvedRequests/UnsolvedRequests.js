import React from "react";
import { useState, useEffect } from "react";
import './UnsolvedRequests.css'


const UnsolvedRequests = (props) => {
    const [requests, setRequests] = useState([]);
    useEffect(() => {
        setRequests(props.requests);
    }, [props.requests, setRequests])

    const renderRequests = () => {
        if (requests)
            return requests.map(i => {
                let classname = "";
                let requst = "";
                if (i.whatRequsted == 0) {
                    classname = "departe";
                    requst = `plane ${i.incPlaneName} wants to departe`;
                }
                else {
                    classname = "arivale";
                    requst = `plane ${i.incPlaneName} wants to arive`;
                }
                return <li key={i.requstId} className={"singleReq " + classname}>{requst} </li>
            });
    }

    return (
        <div className="requests">
            <ul>
                {renderRequests()}
            </ul>
        </div>
    );
}
export default UnsolvedRequests;