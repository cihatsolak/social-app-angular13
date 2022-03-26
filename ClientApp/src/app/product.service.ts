import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Model, Product } from './Model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  serviceUri: string = 'https://localhost:7095/api';
  model = new Model();

  constructor(private httpClient: HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.httpClient.get<Product[]>(`${this.serviceUri}/products/getall`);
  }

  getProductById(id: number): Product {
    return this.model.products.find((product) => product.id === id)!;
  }

  addProduct(product: Product): Observable<Product> {
    return this.httpClient.post<Product>(
      `${this.serviceUri}/products/add`,
      product
    );
  }

  updateProduct(product: Product): Observable<Product> {
    return this.httpClient.put<Product>(
      `${this.serviceUri}/products/update/${product.id}`,
      product
    );
  }

  deleteProduct(id: number) {
    return this.httpClient.delete<Product>(
      `${this.serviceUri}/products/delete/${id}`
    );
  }
}
