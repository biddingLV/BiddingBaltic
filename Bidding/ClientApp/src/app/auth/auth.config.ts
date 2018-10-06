import { environment } from '../../environments/environment';

interface AuthConfig {
  CLIENT_ID: string;
  CLIENT_DOMAIN: string;
  AUDIENCE: string;
  REDIRECT: string;
  SCOPE: string;
}

export const AUTH_CONFIG: AuthConfig = {
  CLIENT_ID: 'RGm50MKxa1XcWeRJdI3Ebgf6l7SBFmz0',
  CLIENT_DOMAIN: 'biddinglv.eu.auth0.com',
  AUDIENCE: `${environment.baseUrl}`,
  REDIRECT: 'http://localhost:4200/callback', // `${environment.baseUrl}/callback`
  SCOPE: 'openid'
};
