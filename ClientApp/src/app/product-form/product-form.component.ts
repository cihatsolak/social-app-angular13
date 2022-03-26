import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../Model';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css'],
})
export class ProductFormComponent implements OnInit {
  @Input() products!: Product[];

  constructor(private productService: ProductService) {}
  ngOnInit(): void {}

  add(name: string, price: string, isactive: boolean) {
    this.productService
      .addProduct(new Product(0, name, parseInt(price), isactive))
      .subscribe((product) => {
        this.products.push(product);
      });
  }
}
