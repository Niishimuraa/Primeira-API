import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Pessoa } from './models/pessoa';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class AppComponent implements OnInit {
  title = 'Consumir-Api';
  http = inject(HttpClient);
  urlApi = 'https://localhost:7026/api/Home';
  pessoas$?: Observable<Pessoa[]>;

  pessoaEncontrada$?: Observable<Pessoa>;
  valorBuscaPessoa = '';

  nomeAdicionar = '';

  obterPessoa() {
    this.pessoas$ = this.http.get<Pessoa[]>(`${this.urlApi}/pessoas`)
  }

  obterPessoaEspecifica() {
    if(!this.valorBuscaPessoa)
      return;

    this.pessoaEncontrada$ = this.http.get<Pessoa>(`${this.urlApi}/pessoas/${this.valorBuscaPessoa}`)
  }

  adicionarPessoa() {
    if(!this.nomeAdicionar)
      return;

    const pessoaCriar: Pessoa = {
      id: "",
      nome: this.nomeAdicionar,
      dataDeCadastro: ""
    }

    this.http.post<void>(`${this.urlApi}/pessoas`, pessoaCriar)
      .subscribe(_ => {
        this.obterPessoa()
        this.nomeAdicionar = ""
      })
  }

  ngOnInit(): void {
    this.obterPessoa();
  }
}
