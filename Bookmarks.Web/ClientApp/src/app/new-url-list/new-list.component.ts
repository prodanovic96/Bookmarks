import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { UrlItem } from '../model/UrlItem.model';
import { UrlList } from '../model/UrlList.model';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import {debounceTime} from 'rxjs/operators';
import { BookmarksComponent } from '../bookmarks/bookmarks.component';

@Component({
  selector: 'app-new-list',
  templateUrl: './new-list.component.html',
  styleUrls: ['./new-list.component.css']
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
  
  private isDisabled : boolean = true;
  private errorStatusCode : number;
  private itemAlreadyExist : boolean = false;

  private http : HttpClient;
  private base : string;

  private isTitleReserved : boolean = false;
  private isListEmpty : boolean = true;
  public isValidUrl : boolean = true;
  
  private subjectKeyUp = new Subject<any>();

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router : Router) {
    this.http = http;
    this.base = baseUrl; 
  }
  
  publish(){
    this.errorStatusCode = 0;

    this.http.post<UrlList>(this.base + 'Url',this.newList).subscribe(
      result => {
        this.newList.title = result.title;
        this.router.navigateByUrl("/" + this.newList.title);
      }, 
      error => {
        this.errorStatusCode=error.status;
        console.error(error)  
      });
  }
  
  changed($event : any){
      const value = $event.target.value;
      this.subjectKeyUp.next(value);
  }

  ngOnInit() {
    this.subjectKeyUp.pipe(debounceTime(1000)).subscribe((d) => {
        this.existing();
    });
  }
  
  existing(){
    this.errorStatusCode = 0;   

    if(this.newList.title != ''){
      this.http.get<UrlList>(this.base + 'Url/namereserved?name=' + this.newList.title)
      .subscribe(
        result => {
          if(!result){
            this.isTitleReserved = false;
            if(!this.isListEmpty){
              this.enableButton();
            }
          }else{
            this.isTitleReserved = true;
            this.errorStatusCode = 404;
            this.disableButton();
          }
        }, 
        error => {
          console.error(error)
        });
    } 
    if(!this.isListEmpty){
      this.enableButton();
    }
  }

  
  addItem(){
    this.itemAlreadyExist = false;

    if(this.newList.items.filter(e => e.link === this.newItem.link).length > 0){
      this.itemAlreadyExist = true;
    }
    
    if(!this.itemAlreadyExist){
      if(!this.isTitleReserved){
        this.enableButton();
      }
      
      let tmp_item = {} as UrlItem;
      tmp_item.link = this.newItem.link;
      tmp_item.description = "This is description!"
      tmp_item.name = "";
  
      this.isListEmpty = false;
      
    if(tmp_item.link.indexOf("http://") === -1 && tmp_item.link.indexOf("https://") === -1)
    {
      tmp_item.link = "https://" + tmp_item.link;
    }

    if(tmp_item.link.indexOf("http://") !== -1)
    {  
      if(tmp_item.link.indexOf("www.") !== -1)
      {
        tmp_item.name = tmp_item.link.replace("http://www.","").slice(0,-4).toUpperCase();
      }
      else
      {
        tmp_item.name = tmp_item.link.replace("http://","").slice(0,-4).toUpperCase();
      }
    }
    else if(tmp_item.link.indexOf("https://") !== -1)
    {
      if(tmp_item.link.indexOf("www.") !== -1)
      {
        tmp_item.name = tmp_item.link.replace("https://www.","").slice(0,-4).toUpperCase();
      }
      else
      {
        tmp_item.name = tmp_item.link.replace("https://","").slice(0,-4).toUpperCase();
      }
    }
      
      if(this.isValidHttpUrl(tmp_item.link)){
        this.newList.items.push(tmp_item);
        this.newItem.link='';
        this.newItem.name='';
      }
    }
  }

  enableButton(){
    this.isDisabled = false;
  }

  disableButton(){
    this.isDisabled = true;
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

    if(this.newList.items.length == 0){
      this.isListEmpty = true;
      this.disableButton();
    }
  }
}
