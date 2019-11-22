import { Injectable } from '@angular/core';
import {HttpClient, HttpParams, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import { User } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly rootUrl = "http://localhost:5000/" //connect to API
  constructor(private http: HttpClient) { }

  registerUser(user : User) : Observable<User> 
  {
    const body : User = {
      FirstName:user.FirstName,
      LastName:user.LastName,
      UserName: user.UserName,
      Password: user.Password,
      Email: user.Email,
      Address: user.Address,
      Token: user.Token
    }
    return this.http.post<User>(this.rootUrl + 'api/User/Register', body);
  }

  getUser(id): Observable<User> {
    return this.http.get<User>(this.rootUrl + 'users/' + id);
  }
}

