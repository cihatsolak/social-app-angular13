import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../Model';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css'],
})
export class ProductDetailsComponent implements OnInit {
  @Input() product!: Product;
  @Input() products!: Product[];
  constructor(private productService: ProductService) {}

  ngOnInit(): void {}

  update(id: number, name: string, price: string, isactive: boolean) {
    let product = new Product(id, name, parseInt(price), isactive);
    this.productService.updateProduct(product).subscribe((result) => {
      let productIndex = this.products.findIndex(
        (product) => product.id === id
      );
      this.products.splice(productIndex, 1, product);
    });
  }
}
