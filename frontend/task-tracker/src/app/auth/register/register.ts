import { ChangeDetectorRef, Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Auth } from '../auth';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  username = '';
  password = '';
  error = '';

  constructor(private auth: Auth, private router: Router, private cdr: ChangeDetectorRef) {}

  submit() {
    this.auth.register(this.username, this.password).subscribe({
      next: () => this.router.navigate(['/login']),
      error: (err: HttpErrorResponse) => {
        this.error = this.extractErrorMessage(err);
        this.cdr.detectChanges();
      }
    });
  }

  private extractErrorMessage(err: HttpErrorResponse): string {
    const backendErrors = err.error?.errors;

    if (!backendErrors) {
      return 'Registration failed';
    }

    return Object.values(backendErrors)
      .flat()
      .join(' ');
  }
}
