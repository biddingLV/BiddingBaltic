// angular
import { Directive, ElementRef, OnInit, Input } from '@angular/core';

// internal
import { PermissionsService } from '../../../../core/services';

@Directive({
  selector: '[hasRole]'
})
export class HasRoleDirective implements OnInit {
  @Input('hasRole') roles: string;

  constructor(private el: ElementRef, private permissionsService: PermissionsService) { }

  ngOnInit(): void {
    const roleArr: string[] = this.roles.split(',').map(str => str.trim());
    this.permissionsService.hasRoles(roleArr).subscribe((data: boolean) => {
      console.log('data: ', data)
      if (!data) {
        this.el.nativeElement.style.display = 'none';
      }
    });
  }
}
