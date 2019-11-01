import { LogType } from '../game-structures/LogType.enum';
export interface Log {
    Id: number;
    Time: Date;
    Type: LogType;
    Message:string;
}
