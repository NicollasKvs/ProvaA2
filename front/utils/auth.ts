import Cookies from 'js-cookie';

export function salvarToken(token: string) {
  Cookies.set('token', token, { expires: 1 });
}

export function removerToken() {
  Cookies.remove('token');
}

export function obterToken(): string | undefined {
  return Cookies.get('token');
}