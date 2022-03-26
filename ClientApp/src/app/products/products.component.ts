import { Component, OnInit } from '@angular/core';
import { Product } from '../Model';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit {
  products!: Product[];
  selectedProduct!: Product;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.getProducts();
  }
  getProducts(): void {
    this.productService.getProducts().subscribe((products) => {
      this.products = products;
    });
  }

  delete(id: number): void {
    this.productService.deleteProduct(id).subscribe((result) => {
      let productIndex = this.products.findIndex(
        (product) => product.id === id
      );
      this.products.splice(productIndex, 1);
    });
    this.ngOnInit();
  }

  onSelectProduct(product: Product) {
    this.selectedProduct = product;
  }
}
