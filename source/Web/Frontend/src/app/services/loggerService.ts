import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {map, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class LoggerService {
  constructor(private http: HttpClient) {
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
