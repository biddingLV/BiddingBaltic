interface AuthConfig {
  CLIENT_ID: string;
  CLIENT_DOMAIN: string;
  AUDIENCE: string;
  REDIRECT: string;
  SCOPE: string;
}

export const AUTH_CONFIG: AuthConfig = {
  CLIENT_ID: '[AUTH0_CLIENT_ID]',
  CLIENT_DOMAIN: '[AUTH0_CLIENT_DOMAIN]', // e.g., you.auth0.com
  AUDIENCE: 'https://localhost:44310/api', // e.g., http://localhost:8083/api/
  REDIRECT: 'https://localhost:44310/api/callback',
  SCOPE: 'openid profile'
};
