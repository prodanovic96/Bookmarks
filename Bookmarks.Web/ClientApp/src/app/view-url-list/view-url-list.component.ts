import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { UrlList } from '../model/UrlList.model';

@Component({
  selector: 'app-view-url-list',
  templateUrl: './view-url-list.component.html',
  styleUrls: ['./view-url-list.component.css']
})

export class ViewUrlListComponent implements OnInit{
  public get_list : UrlList;
  public title:string;
  private http : HttpClient;
  private base : string;
  private err : number;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router : Router){
    this.http = http;
    this.base = baseUrl;
  }

  getList(naslov : string) {
    this.err = 0;
    
    this.http.get<UrlList>(this.base + 'Url/get?name=' + naslov).subscribe(
    result => {
      this.get_list = result;
    }, 
    error => {
      this.err=error.status;
      console.error(error)  
    });
  }

  ngOnInit() {
    this.title = this.router.url;
    this.title = this.title.substring(1);

    this.getList(this.title);
  }
}
