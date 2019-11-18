// angular
import { Directive, OnInit, Input, ViewContainerRef, TemplateRef } from '@angular/core';
import { PermissionService } from 'ClientApp/src/app/core/services/permissions/permission.service';

// internal


/** ATM only supports one role! */
@Directive({ selector: '[hasRole]' })
export class HasRoleDirective implements OnInit {
  @Input('hasRole') role: string;

  constructor(private permissionService: PermissionService, private viewContainer: ViewContainerRef, private templateRef: TemplateRef<any>) { }

  ngOnInit(): void {
    // this.permissionService.hasRoleName(this.role).subscribe(
    //   (data: boolean) => {
    //     if (data) {
    //       this.viewContainer.createEmbeddedView(this.templateRef);
    //     } else {
    //       this.viewContainer.clear();
    //     }
    //   });
  }
}
