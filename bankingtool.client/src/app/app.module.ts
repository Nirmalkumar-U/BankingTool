import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { LoaderComponent } from './component/loader/loader.component';
import { NoteComponent } from './component/note/note.component';
import { NotificationPopupComponent } from './core/component/notification-popup/notification-popup.component';
import { SharedModule } from './core/component/shared.module';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [
    AppComponent,
    LoaderComponent,
    NotificationPopupComponent,
    NoteComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    CoreModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    SharedModule // Add for shared components/modules
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
