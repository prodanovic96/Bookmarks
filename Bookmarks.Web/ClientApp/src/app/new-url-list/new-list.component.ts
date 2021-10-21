import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit, Input } from '@angular/core';
import { UrlItem } from '../model/UrlItem.model';
import { UrlList } from '../model/UrlList.model';

@Component({
  selector: 'app-new-list',
  templateUrl: './new-list.component.html',
  styleUrls: ['./new-list.component.css'],
})
export class NewUrlListComponent implements OnInit {

  newList : UrlList ={
    title : '',
    description : '',
    items : []
  };

  newItem : UrlItem = {
    name: '',
    description : '',
    link : ''
  };
  private http : HttpClient;
  private base : string;
  public isValidUrl : boolean = true;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.base = baseUrl;
    
  }

  publish(){
    this.http.post<UrlList>(this.base + 'Url',this.newList).subscribe(error => {
      console.error(error)});
  }

  addItem(){
    let novi = {} as UrlItem;
    novi.link = this.newItem.link;
    if(this.isValidHttpUrl(novi.link))
      this.newList.items.push(novi);

  }

 isValidHttpUrl(string : string) {
    this.isValidUrl = true;
    let url;
    var lastFour = string.substr(string.length-4);
    if(lastFour !== ".com" && lastFour !== ".net" && lastFour !== ".edu" && lastFour !== ".org")
    {
      this.isValidUrl = false;
      return false;
    }

    try {
      url = new URL(string);

      this.isValidUrl = true;
    } catch (_) {
      this.isValidUrl = false;
      return false;  
    }
  
    return url.protocol === "http:" || url.protocol === "https:";
  }

  deleteItem(item)
  {
    var index = this.newList.items.indexOf(item);
    this.newList.items.splice(index, 1);
  }

  ngOnInit() {
  }

}

