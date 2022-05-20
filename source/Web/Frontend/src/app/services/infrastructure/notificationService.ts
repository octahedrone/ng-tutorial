import {Injectable} from "@angular/core";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(private snackBar: MatSnackBar) {
  }

  success(message: string) {
    this.snackBar.open(message, 'Dismiss', {
      duration: 1500,
      panelClass:['success-snackbar']
    });
  }

  error(message: string) {
    this.snackBar.open('Your message here', 'Dismiss', {
      duration: 2000,
      panelClass:['error-snackbar']
    });
  }
}
