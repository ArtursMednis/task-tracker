import { Routes } from '@angular/router';
import { authGuard } from './auth/auth-guard';
import { Login } from './auth/login/login';
import { Register } from './auth/register/register';
import { TaskList } from './tasks/task-list/task-list';
import { TaskForm } from './tasks/task-form/task-form';

export const routes: Routes = [
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  {
    path: 'tasks',
    component: TaskList,
    canActivate: [authGuard]
  },
  {
    path: 'tasks/:id',
    component: TaskForm,
    canActivate: [authGuard]
  },
  {
    path: 'tasks/new',
    component: TaskForm,
    canActivate: [authGuard]
  },
  { path: '', redirectTo: 'tasks', pathMatch: 'full' }
];

