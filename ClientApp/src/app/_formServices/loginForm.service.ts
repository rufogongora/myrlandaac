import { Injectable } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Account } from "../_models/account.model";

@Injectable({
    providedIn: 'root'
})
export class LoginFormService {
    
    private form: FormGroup;
    get f() { return this.form; }

    constructor(private fb: FormBuilder) {
        this.form = this.fb.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });    
    }

}