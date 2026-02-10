import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Api } from '../services/api';

@Injectable({
  providedIn: 'root',
})

export class Auth {
  currentUser$ = new BehaviorSubject<string | null>(null);
  isUserAuthenticated = new BehaviorSubject<boolean | null>(null);

  constructor(private api: Api) {}

  login(username: string, password: string) {
    this.isUserAuthenticated.next(null);
    return this.api.post<void>('/Account/login', { username, password });
  }

  register(username: string, password: string) {
    return this.api.post<void>('/Account/register', { username, password });
  }

  logout() {
    return this.api.post<void>('/Account/logout', {});
  }

  loadCurrentUser() {
    this.api.get<{ username: string }>('/Account/current-user')
      .subscribe({
        next: res => {
          this.currentUser$.next(res.username);
          this.isUserAuthenticated.next(true);
        },
        error: () => {
          this.currentUser$.next(null);
          this.isUserAuthenticated.next(false);
        }
      });
  }
}
