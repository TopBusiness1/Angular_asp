import { Component, OnInit } from '@angular/core';
import { User } from '../shared/user.model';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../shared/user.service';
import { AuthService } from '../services/auth.service';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {
  user:User;
  userLoginForm: FormGroup;
  loading = false;
  returnUrl: string;

  constructor(private userService:UserService, 
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService
  ) { 
    //if (this.authService.currentUserValue) {
    //  this.router.navigate(['/']);
    //}
  }

  ngOnInit() {
    this.createLoginForm();
  }
  
  createLoginForm() {
    this.userLoginForm = this.fb.group({
      UserName:  ['', Validators.required],
      Password:  ['', Validators.required]
  });
  this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
} 

  get f() { return this.userLoginForm.controls; }

  onSubmit() {
    let request : User = new User();
    request.UserName = this.userLoginForm.get('UserName').value;
    request.Password = this.userLoginForm.get('Password').value;

    if (this.userLoginForm.valid) {
      this.loading = true;
      //this.user = Object.assign({}, this.userLoginForm.value);
      this.authService.login(request)
        .pipe(first())
        .subscribe(
          (data:any) => {
            this.toastr.success("Login Successful");
          }
        , 
        error => {
        this.toastr.error(error);
        this.loading = false;
    }); 
  }
  }


}

