interface AuthConfig {
  clientID: string;
  domain: string;
  callbackURL: string;
  apiUrl: string;
}

export const AUTH_CONFIG: AuthConfig = {
  clientID: 'GjK056TSEyB4McMZ2w5VWgGca3hkYGIC',
  domain: 'bidding.eu.auth0.com',
  callbackURL: 'http://localhost:4200/auctions',
  apiUrl: 'http://localhost:3010'
};
