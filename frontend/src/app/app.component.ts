import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Pessoa } from './models/pessoa';
import { Observable, tap } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule, ReactiveFormsModule],
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

  pessoaAdicionada$?: Observable<Pessoa>;
  nomeAdicionar = '';

  pessoaDeletada$?: Observable<Pessoa>;
  idDeletar = '';
  
  pessoaAtualizada$?: Observable<Pessoa>;
  idAtualizar = '';
  nomeAtualizar = '';

  //Procurar todos
  obterPessoa() {
    this.pessoas$ = this.http.get<Pessoa[]>(`${this.urlApi}/pessoas`)
  }

  //Procurar
  obterPessoaEspecifica() {
    if(!this.valorBuscaPessoa)
      return;

    this.pessoaEncontrada$ = this.http.get<Pessoa>(`${this.urlApi}/pessoas/${this.valorBuscaPessoa}`)
  }

  //Adicionar
  adicionarPessoa() {
    if(!this.nomeAdicionar)
      return;

    const pessoaCriar: Pessoa = {
      id: "",
      nome: this.nomeAdicionar,
      dataDeCadastro: ""
    }

    this.pessoaAdicionada$ = this.http.post<Pessoa>(`${this.urlApi}/pessoas`, pessoaCriar)
      .pipe(
        tap(pessoa => {
          this.obterPessoa()
          this.nomeAdicionar = ""
        }
      )
    )
  }

  //Deletar
  deletarPessoa() {
      if(!this.idDeletar)
    return;
    
    this.pessoaDeletada$ = this.http.delete<Pessoa>(`${this.urlApi}/pessoas/${this.idDeletar}`)
      .pipe(
        tap(pessoa => {
          this.obterPessoa()
          this.idDeletar = ""
        }
      )
    )
  }

  //Atualizar
  meuForm = new FormGroup({
    id: new FormControl(''),
    nome: new FormControl(''),
  })

  atualizarPessoa() {
    const nome = this.meuForm.value.nome ?? '';

    const pessoaAtualizar: Pessoa = {
      id: "",
      nome: nome,
      dataDeCadastro: ''
    };

    this.pessoaAtualizada$ = this.http.put<Pessoa>(`${this.urlApi}/pessoas/${this.meuForm.value.id}`, 
      pessoaAtualizar)
      .pipe(
        tap(pessoa => {
          this.obterPessoa()
          this.meuForm.reset();
        })
      )
    
  }

  ngOnInit(): void {
    this.obterPessoa();
  }
}
