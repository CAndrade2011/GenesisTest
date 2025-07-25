import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalculadoraService {
  private apiUrl = environment.apiUrl;
  private apiKey = environment.apiKey;

  constructor(private http: HttpClient) { }

  calcularCdb(valorInicial: number, prazoMeses: number): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'API-Key': this.apiKey
    });
    const body = { valorInicial, prazoMeses };
    return this.http.post(this.apiUrl, body, { headers });
  }
}
