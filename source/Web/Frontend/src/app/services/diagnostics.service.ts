import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable({providedIn: "root"})
export class DiagnosticsService {
  constructor(
    private readonly http: HttpClient) {
  }

  datetime = () => this.http.get<string>("diagnostics/datetime");
}
