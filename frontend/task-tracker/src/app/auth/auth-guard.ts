import { CanActivateFn, Router } from '@angular/router';
import { filter, map, take } from 'rxjs';
import { Auth } from './auth';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = () => {
  const auth = inject(Auth);
  const router = inject(Router);

  auth.loadCurrentUser();

  return auth.isUserAuthenticated.pipe(
    filter(isAuthenticated => isAuthenticated !== null),
    take(1),
    map(isAuthenticated => {
       if (isAuthenticated) 
        return true;

       router.navigate(['/login']);
       return false;
    })
  );
};
