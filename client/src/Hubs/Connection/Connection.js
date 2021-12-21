import {HubConnectionBuilder,LogLevel } from "@microsoft/signalr";

const Connect =(setConnection,setRequstHistory,setStation)=>{
    let conection = new HubConnectionBuilder().
    withUrl("http://localhost:4208/MainHub").
    configureLogging(LogLevel.Information).
    build();

    conection.on("AirPortChanged",(airport)=>{
        if(airport!==null && airport!=='undefiend'){
            setStation(airport);
        }
    });

    conection.on("Test",(test)=>{
        console.log(`got to test ${test}`);
    });
    conection.on("RequstListUpdate",(requsts)=>{
        setRequstHistory(requsts);
    });
    conection.start();
    setConnection(conection);
    console.log(`got to postconection start ${conection}`);
}

export default Connect;