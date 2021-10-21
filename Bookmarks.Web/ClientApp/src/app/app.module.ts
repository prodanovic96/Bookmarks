import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BookmarksComponent } from './bookmarks/bookmarks.component';
import { AssignmentComponent } from './assignment/assignment.component';
import { NewUrlListComponent } from './new-url-list/new-list.component';
import { ViewUrlListComponent } from './view-url-list/view-url-list.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    BookmarksComponent,
    AssignmentComponent,
    NewUrlListComponent,
    ViewUrlListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'new-url-list',component:NewUrlListComponent},
      {path: 'view-url-list',component:ViewUrlListComponent}
      /*{ path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'bookmarks', component: BookmarksComponent },
      { path: 'assignment', component: AssignmentComponent },*/
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
