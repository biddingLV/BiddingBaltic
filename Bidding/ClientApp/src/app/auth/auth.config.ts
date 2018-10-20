import { environment } from '../../environments/environment';
// import { ENV } from './../core/env.config';

interface AuthConfig {
  CLIENT_ID: string;
  CLIENT_DOMAIN: string;
  AUDIENCE: string;
  REDIRECT: string;
  // SCOPE: string;
  // NAMESPACE: string;
};

// export const AUTH_CONFIG: AuthConfig = {
//   CLIENT_ID: 'RGm50MKxa1XcWeRJdI3Ebgf6l7SBFmz0',
//   CLIENT_DOMAIN: 'biddinglv.eu.auth0.com',
//   AUDIENCE: `${environment.baseUrl}`,
//   REDIRECT: 'http://localhost:4200/callback', // `${environment.baseUrl}/callback`
//   SCOPE: 'openid'
// };

export const AUTH_CONFIG: AuthConfig = {
  CLIENT_ID: 'RGm50MKxa1XcWeRJdI3Ebgf6l7SBFmz0',
  CLIENT_DOMAIN: 'biddinglv.eu.auth0.com', // e.g., kmaida.auth0.com
  AUDIENCE: `${environment.baseUrl}`, // e.g., http://localhost:8083/api/
  REDIRECT: 'http://localhost:4200/callback',//`${ENV.BASE_URI}/callback`,
  // SCOPE: 'openid profile email',
  //NAMESPACE: 'http://myapp.com/roles'
};