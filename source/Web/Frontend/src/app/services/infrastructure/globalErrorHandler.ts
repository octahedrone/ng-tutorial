import {ErrorHandler} from "@angular/core";
import {NotificationService} from "./notificationService";
import {HttpClient} from "@angular/common/http";
import {map, Observable} from "rxjs";

export class GlobalErrorHandler implements ErrorHandler {
  constructor(private http: HttpClient,
              private notificationService: NotificationService) {
  }

  handleError(error: any) {
    this.log(error).subscribe();
    this.notificationService.error('Error happened');
  }

  log(error: any): Observable<boolean> {
    return this.http
      .post('logs/error', error, {
          observe: 'response'
        }
      )
      .pipe(map(e => true));
  }
}
