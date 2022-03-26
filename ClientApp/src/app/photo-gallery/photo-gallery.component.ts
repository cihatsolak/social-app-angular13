import { Component, Input, OnInit } from '@angular/core';
import { Image } from '../_models/Image';

@Component({
  selector: 'app-photo-gallery',
  templateUrl: './photo-gallery.component.html',
  styleUrls: ['./photo-gallery.component.css'],
})
export class PhotoGalleryComponent implements OnInit {
  @Input() images?: Image[];
  constructor() {}

  ngOnInit(): void {}
}
