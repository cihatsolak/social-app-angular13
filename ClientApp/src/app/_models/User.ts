import { Image } from './Image';

export class User {
  id!: number;
  username!: string;
  name!: string;
  age!: number;
  gender!: string;
  createdDate!: Date;
  lastActive!: Date;
  city!: string;
  country!: string;
  introduction!: string;
  hobbies!: string;
  image!: Image;
}
