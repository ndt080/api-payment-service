import { Component } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { SubscribeModalComponent } from "@features/pages/home/components/subscribe-modal/subscribe-modal.component";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent {
  constructor(public dialog: MatDialog) {}

  openModal() {
    const dialogRef = this.dialog.open(SubscribeModalComponent);
    dialogRef.afterClosed().subscribe();
  }
}
