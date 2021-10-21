import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { UrlItem } from '../model/UrlItem.model';
import { UrlList } from '../model/UrlList.model';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import {debounceTime} from 'rxjs/operators';

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
  private err : number;

  private http : HttpClient;
  private base : string;

  private isValide : boolean = true;
  private isEmpty : boolean = true;
  
  private subjectKeyUp = new Subject<any>();

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router : Router) {
    this.http = http;
    this.base = baseUrl; 
  }
  
  publish(){
    this.err = 0;

    this.http.post<UrlList>(this.base + 'Url',this.newList).subscribe(
      result => {
        this.newList.title = result.title;
        this.router.navigateByUrl("/" + this.newList.title);
      }, 
      error => {
        this.err=error.status;
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
    this.err = 0;   

    if(this.newList.title != ''){
      this.http.get<UrlList>(this.base + 'Url/existing?name=' + this.newList.title)
      .subscribe(
        _ => {
          if(!this.isEmpty){
            this.enableButton();
          }
        }, 
        error => {
          this.err=error.status;
          this.disableButton();
          console.error(error)  
        });
    } 
    if(!this.isEmpty){
      this.enableButton();
    }
  }

  addItem(){

    if(this.isValide){
      this.enableButton();
    }
    
    this.urlValide(this.newItem.link);

    let tmp_item = {} as UrlItem;
    tmp_item.link = this.newItem.link;

    this.isEmpty = false;

    this.newList.items.push(tmp_item);
    this.newItem.link='';
  }

  urlValide(item_link : string){
    const control: HTMLInputElement = document.createElement("input");
    control.type = "url";
    control.value = item_link;
    const isValid: boolean = control.checkValidity();
  }

  enableButton(){
    this.isDisabled = false;
  }

  disableButton(){
    this.isDisabled = true;
  }
}