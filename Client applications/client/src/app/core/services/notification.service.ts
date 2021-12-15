import { Injectable } from "@angular/core";
import { ToastrService } from "ngx-toastr";

@Injectable({
  providedIn: "root",
})
export class NotificationService {

  constructor(private toastrService: ToastrService) {
  }

  showSuccess(message: string, title: string) {
    this.toastrService.success(message, title, {
      timeOut: 3000,
    });
  }

  showError(message: string, title: string) {
    this.toastrService.error(message, title, {
      timeOut: 3000,
    });
  }

  showInfo(message: string, title: string) {
    this.toastrService.info(message, title, {
      timeOut: 3000,
    });
  }

  showWarning(message: string, title: string) {
    this.toastrService.warning(message, title, {
      timeOut: 3000,
    });
  }
}
