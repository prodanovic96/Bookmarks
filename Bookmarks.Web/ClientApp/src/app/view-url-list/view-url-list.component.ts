import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { UrlList } from '../model/UrlList.model';

@Component({
  selector: 'app-view-url-list',
  templateUrl: './view-url-list.component.html',
  styleUrls: ['./view-url-list.component.css']
})
export class ViewUrlListComponent implements OnInit {
  public get_list : UrlList;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<UrlList>(baseUrl + 'UrlList').subscribe(result => {
      this.get_list = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
