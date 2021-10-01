import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-bookmarks',
  templateUrl: './bookmarks.component.html',
  styleUrls: ['./bookmarks.component.css']
})
export class BookmarksComponent {
  public dummyData: DummyData;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<DummyData>(baseUrl + 'dummy').subscribe(result => {
      this.dummyData = result;
    }, error => console.error(error));
  }
}

interface DummyData {
  name: string;
  surname: string;
  age: number;
}