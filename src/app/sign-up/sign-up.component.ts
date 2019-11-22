import { Component, OnInit } from '@angular/core';
import { User } from '../shared/user.model';
import { NgForm, FormGroup, ReactiveFormsModule, FormBuilder, FormControl, Validators } from '@angular/forms';
import { UserService } from '../shared/user.service';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-sign-up', //used in app.component.html
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  user:User;
  userRegistrationForm: FormGroup;
  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";

  constructor(private userService:UserService, private toastr: ToastrService, private fb: FormBuilder) { }

  ngOnInit() {
    this.createRegisterForm();
  }
  
  createRegisterForm() {
    this.userRegistrationForm = this.fb.group({
      FirstName: ['', Validators.required],
      LastName:  ['', Validators.required],
      UserName:  ['', Validators.required],
      Password:  ['', Validators.required],
      Email: ['', Validators.required],
      Address: ['', Validators.required]
  });
} 

  onSubmit() {
    let request : User = new User();
    request.FirstName = this.userRegistrationForm.get('FirstName').value;
    request.LastName = this.userRegistrationForm.get('LastName').value;
    request.UserName = this.userRegistrationForm.get('UserName').value;
    request.Password = this.userRegistrationForm.get('Password').value;
    request.Email = this.userRegistrationForm.get('Email').value;
    request.Address = this.userRegistrationForm.get('Email').value;

    if (this.userRegistrationForm.valid) {
      //this.user = Object.assign({}, this.userRegistrationForm.value);
      this.userService.registerUser(request).subscribe((data:any) => {      
        this.toastr.success("User Registration Successful"); 
      }, error => {
        this.toastr.error(error);
    }); 
    }
  }


}
