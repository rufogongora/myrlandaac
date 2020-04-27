import { Player } from "./player.model";

export class Account {
    id: number;
    name: string;
    players: Player[];
    premdays: number;
}