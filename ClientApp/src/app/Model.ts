export class Model {
  products: Product[];

  constructor() {
    this.products = [
      new Product(1, 'Samsung S5', 2599, false),
      new Product(2, 'Iphone 11 Pro Max', 13999, true),
      new Product(3, 'Xiamio Mi 11', 7899, true),
      new Product(3, 'Nokia 3310', 250, true),
    ];
  }
}

export class Product {
  id: number;
  name: string;
  price: number;
  isActive: boolean;

  constructor(id: number, name: string, price: number, isActive: boolean) {
    this.id = id;
    this.name = name;
    this.price = price;
    this.isActive = isActive;
  }
}
