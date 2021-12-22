import { Subscription } from "@models/subscription.model";

export interface User {
  id?: any;
  email: string;
  jwtToken?: string;
  refreshToken?: string;
  password?: string;
  subscriptions?: Subscription[];
}
