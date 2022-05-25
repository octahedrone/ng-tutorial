import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {QueryResponse} from "./service.models";

@Injectable({providedIn: "root"})
export class AdventureLogsService {
  constructor(private readonly http: HttpClient) {
  }

  getAdventureLog = (): Observable<QueryResponse<AdventureLogRecord[]>> =>
    this.http.get<QueryResponse<AdventureLogRecord[]>>("adventure/logs");
}

export interface AdventureLogRecord {
  cardText: string;
  selectedOptionIndex: number;
  options: string[]
}

