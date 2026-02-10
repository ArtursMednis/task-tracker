import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Auth } from '../../auth/auth';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {

  constructor(
    public auth: Auth,
    private router: Router
  ) {}

  logout() {
    this.auth.logout().subscribe(() => {
      this.auth.currentUser$.next(null);
      this.router.navigate(['/login']);
    });
  }
}
