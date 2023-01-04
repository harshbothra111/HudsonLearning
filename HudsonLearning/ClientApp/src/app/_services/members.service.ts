import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { of, map } from 'rxjs';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }
  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + "users").pipe(
      map(members => {
        return members;
      })
    );
  }
  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + "users/" + username);
  }
  updateMember(member: Member) {
    return this.http.put(this.baseUrl + "users", member).pipe(
      map(() => {
      })
    );
  }
}
