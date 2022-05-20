import {ErrorHandler} from "@angular/core";
import {LoggerService} from "../loggerService";
import {NotificationService} from "./notificationService";

export class GlobalErrorHandler implements ErrorHandler {
  constructor(private loggerService: LoggerService,
              private notificationService: NotificationService) {
  }

  handleError(error: any) {
    this.loggerService.log(error).subscribe();
    this.notificationService.error('Error happened');
  }
}
