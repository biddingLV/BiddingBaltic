// angular
import { Component, OnInit, Input } from "@angular/core";

// internal
import { BreadcrumbItem } from "../../models/breadcrumb-item.model";

@Component({
  selector: "app-breadcrumb",
  templateUrl: "./breadcrumb.component.html",
  styleUrls: ["./breadcrumb.component.scss"]
})
export class BreadcrumbComponent implements OnInit {
  @Input() breadcrumbs: BreadcrumbItem[];

  constructor() {}

  ngOnInit() {}
}
