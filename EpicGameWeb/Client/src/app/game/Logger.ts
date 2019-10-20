import {DataToLog} from './DataToLog';
export class Logger {
    
    public Data:DataToLog[] = [];
    public constructor() {}
  
    public PushDefaultMsg(data:string) : void
    {
        let date = new Date();
        let dataToLog :DataToLog= new DataToLog();
        dataToLog.message = 
            "[Default] " +
            data + " " + 
            date.getUTCHours() + ":" +
            date.getMinutes() + ":" +
            date.getSeconds() +
            "  [" + 
            date.getUTCDate() + "." + 
            date.getMonth() + "." + 
            date.getFullYear()%100 
            + "]";
        dataToLog.color = 0;
        this.Data.push(dataToLog);
    }

    public PushBuildingMsg(data:string) : void
    {
        let date = new Date();
        let dataToLog :DataToLog= new DataToLog();
        dataToLog.message = 
            "[Build] " +
            data + " " + 
            date.getUTCHours() + ":" +
            date.getMinutes() + ":" +
            date.getSeconds() +
            "  [" + 
            date.getUTCDate() + "." + 
            date.getMonth() + "." + 
            date.getFullYear()%100 
            + "]";
        dataToLog.color = 1;
        this.Data.push(dataToLog);
    }

    public PushWarMsg(data:string) : void
    {
        let date = new Date();
        let dataToLog :DataToLog= new DataToLog();
        dataToLog.message = 
            "[War] " +
            data + " " + 
            date.getUTCHours() + ":" +
            date.getMinutes() + ":" +
            date.getSeconds() +
            "  [" + 
            date.getUTCDate() + "." + 
            date.getMonth() + "." + 
            date.getFullYear()%100 
            + "]";
        dataToLog.color = 2;
        this.Data.push(dataToLog);
    }

    public PushCommunicationMsg(data:string) : void
    {
        let date = new Date();
        let dataToLog :DataToLog= new DataToLog();
        dataToLog.message = 
            "[Communication] " +
            data + " " + 
            date.getUTCHours() + ":" +
            date.getMinutes() + ":" +
            date.getSeconds() +
            "  [" + 
            date.getUTCDate() + "." + 
            date.getMonth() + "." + 
            date.getFullYear()%100 
            + "]";
        dataToLog.color = 3;
        this.Data.push(dataToLog);
    }
}