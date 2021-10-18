import { Component, OnInit } from '@angular/core';
import { UrlHandlingStrategy } from '@angular/router';
import { UrlItem } from '../model/UrlItem.model';
import { UrlList } from '../model/UrlList.model';

@Component({
  selector: 'app-new-list',
  templateUrl: './new-list.component.html',
  styleUrls: ['./new-list.component.css']
})
export class NewUrlListComponent implements OnInit {

  list : UrlList ={
    title : '',
    description : '',
    items : null
  };

  item : UrlItem = {
    name: '',
    description : '',
    link : ''
  }
  
  publish(){
    // prosledi UrlList na backend
  }

  addItem(){
    //  pokupiti name i description iz metadata
    this.list.items.push(this.item);
  }

  constructor() { }

  ngOnInit() {
  }

}
