import {Account} from './account.model';
export class User {
    account: Account;
    role: number;
    token?: string;
}