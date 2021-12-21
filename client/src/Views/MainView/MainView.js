import React from "react";
import Airplane from "../../Components/Airplane/Airplane";
import UnsolvedRequests from "../../Components/UnsolvedRequests/UnsolvedRequests";
import Connect from "../../Hubs/Connection/Connection";
import { useState,useEffect } from "react";
import './MainView.css'



const MainView =()=>{
const [connection,setConnection] = useState();
const [airPortStaions,setAirPortStaions] = useState([]);
const [requstHistory,setRequstHistory] = useState([]);

useEffect(()=>{
    Connect(setConnection,setRequstHistory,setAirPortStaions);
},[setConnection,setRequstHistory,setAirPortStaions]);


return(
    <div className="MainView">
        <Airplane stations={airPortStaions}/>
        <UnsolvedRequests requests={requstHistory} />
    </div>
);

}

export default MainView;