import { ChangeDetectorRef, Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Auth } from '../auth';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  username = '';
  password = '';
  error = '';

  constructor(private auth: Auth, private router: Router, private cdr: ChangeDetectorRef) {}

  submit() {
    this.auth.login(this.username, this.password).subscribe({
      next: () => {
        this.router.navigate(['/tasks']);
      },
      error: () => {
        this.error = 'Nepareizs lietotājvārds vai parole';
        this.cdr.detectChanges();
      }
    });
  }
}
