import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ComingSoonListComponent } from './containers/coming-soon-list/list.component';
import { ComingSoonSurveyComponent } from './components/coming-soon/survey/survey.component';

const routes: Routes = [
  { path: '', component: ComingSoonListComponent },
  { path: 'aptaujas-anketa', component: ComingSoonSurveyComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
