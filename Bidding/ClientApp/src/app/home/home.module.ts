import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { SharedModule } from '../shared/shared.module';
import { BlogListComponent } from '../blog/containers/list/list.component';
import { ComingSoonListComponent } from './containers/coming-soon-list/list.component';
import { ComingSoonSurveyComponent } from './components/coming-soon/survey/survey.component';
import { HomeListComponent } from './containers/list/list.component';
import { WelcomeHeaderComponent } from './components/welcome-header/welcome-header.component';
import { SubscribeEmailComponent } from './components/coming-soon/subscribe/email/email.component';
import { SubscribeWhatsappComponent } from './components/coming-soon/subscribe/whatsapp/whatsapp.component';

@NgModule({
  imports: [
    CommonModule,
    HomeRoutingModule,
    SharedModule
  ],
  exports: [

  ],
  declarations: [
    BlogListComponent,
    ComingSoonListComponent,
    ComingSoonSurveyComponent,
    HomeListComponent,
    WelcomeHeaderComponent,
    SubscribeEmailComponent,
    SubscribeWhatsappComponent
  ]
})
export class HomeModule { }
