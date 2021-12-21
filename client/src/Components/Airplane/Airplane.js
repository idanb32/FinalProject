import React from "react";
import Station from "../../UIElements/Station/Station";
import { useEffect,useState } from "react";
import './Airplane.css'

const Airplane =(props)=>{
    const [stations,setStations] = useState([]);
    useEffect(()=>{
        setStations(props.stations)
    },[props.stations,setStations])

    const renderStations =()=>{
        let item=0;
        return stations.map(i=>{
            item++;
           return( <li key={i.stationNum} className={"Grid"+item}>
                <Station Plane={i.plane ? i.plane.name:""} stationNum={i.stationNum} />
            </li>)
        });
    };


    return(
        <div className="airplane">
                {renderStations()}
        </div>
    );

};
export default Airplane;