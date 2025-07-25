import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CalculadoraService } from './services/calculadora.service';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  valorInicial: any = 0;
  prazoMeses: any = 0;
  resultado: any = null;
  loading = false;
  errors: string[] = [];
  apiUrl = environment.apiUrl;

  constructor(private calculadoraService: CalculadoraService) {}

  calcular() {
    this.errors = [];
    this.resultado = null;
    // Validação para valorInicial
    if (this.valorInicial === null || this.valorInicial === undefined || this.valorInicial === '' || isNaN(Number(this.valorInicial)) || Number(this.valorInicial) <= 0) {
      this.errors.push('O valor inicial deve ser maior que zero.');
    }
    // Validação para prazoMeses
    if (this.prazoMeses === null || this.prazoMeses === undefined || this.prazoMeses === '' || isNaN(Number(this.prazoMeses)) || Number(this.prazoMeses) < 2) {
      this.errors.push('O prazo deve ser superior ou igual a 2 meses.');
    }
    if (this.errors.length > 0) {
      return;
    }
    this.loading = true;
    this.calculadoraService.calcularCdb(Number(this.valorInicial), Number(this.prazoMeses)).subscribe({
      next: res => {
        this.resultado = res;
        this.loading = false;
      },
      error: err => {
        this.errors = [(err.error?.message || 'Erro ao calcular CDB') + `\nURL da API: ${this.apiUrl}`];
        this.loading = false;
      }
    });
  }
}
