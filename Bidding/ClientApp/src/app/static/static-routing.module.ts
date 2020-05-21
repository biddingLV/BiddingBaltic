import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// internal
import { FAQPageComponent } from './components/faq-page/faq-page.component';

const routes: Routes = [{ path: '/faq', component: FAQPageComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StaticRoutingModule {}
