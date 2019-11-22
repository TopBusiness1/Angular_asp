import { Injectable } from '@angular/core';
import { Product } from '../entities/product.entity';

@Injectable()
export class ProductService {
    private products: Product[];

    constructor() {
        this.products = [
        { id: 'p01', name: 'name 1', price: 100, photo: 'dummy1.jpg' },
        { id: 'p02', name: 'name 2', price: 200, photo: 'dummy2.jpg' },
        { id: 'po3', name: 'name 3', price: 300, photo: 'dummy3.jpg' }
        ];
    }

    findAll(): Product[] {
        return this.products;
    }

    find(id: string): Product {
        return this.products[this.getSelectedIndex(id)];
    }

    private getSelectedIndex(id: string) {
        for (var i = 0; i < this.products.length; i++) {
            if (this.products[i].id == id) {
                return i;
            }
        }
        return -1;
    }
}