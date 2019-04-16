// angular
import { Directive, OnInit, Input, ViewContainerRef, TemplateRef } from '@angular/core';

// internal
import { PermissionsService } from '../../../../core/services';

/** ATM only supports one role! */
@Directive({ selector: '[hasRole]' })
export class HasRoleDirective implements OnInit {
  @Input('hasRole') role: string;

  constructor(private permissionsService: PermissionsService, private viewContainer: ViewContainerRef, private templateRef: TemplateRef<any>) { }

  ngOnInit(): void {
    this.permissionsService.hasRoleName(this.role).subscribe(
      (data: boolean) => {
        if (data) {
          this.viewContainer.createEmbeddedView(this.templateRef);
        } else {
          this.viewContainer.clear();
        }
      });
  }
}
