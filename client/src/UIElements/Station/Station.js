import React from "react";
import { useState,useEffect } from "react";
import './Station.css'

const Station =(props)=>{
    
    useEffect(()=>{
        setPlaneId(props.Plane);
        setStationNum(props.stationNum);
    },[props.Plane,props.stationNum]);
    const [planeId, setPlaneId] = useState("");
    const [stationNum,setStationNum]=useState("");
    
    const planeMassage =()=>{
        if(planeId!="")
        return "Plane id: "+planeId;
        return "";
    }
    
    const renderPlane =()=>{
        return(
            <div className={planeId!="" ? "plane":""}>
                {planeMassage()}
            </div>
        );
    };

    return(
        <div className={"station stationNum"+stationNum}>
            {renderPlane()}
        </div>
    )
}
export default Station;
