import { LogType } from '../game-structures/LogType.enum';
export interface Log {
    Id: number;
    Time: string;
    Type: LogType;
    Message:string;
}
