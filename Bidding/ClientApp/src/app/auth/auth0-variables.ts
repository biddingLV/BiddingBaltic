interface AuthConfig {
  clientID: string;
  domain: string;
  callbackURL: string;
  apiUrl: string;
}

export const AUTH_CONFIG: AuthConfig = {
  clientID: 'RGm50MKxa1XcWeRJdI3Ebgf6l7SBFmz0',
  domain: 'biddinglv.eu.auth0.com',
  callbackURL: 'http://localhost:4200/callback',
  apiUrl: 'http://localhost:61244'
};

