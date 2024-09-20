import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  // public evento: any = {
    //   Tema: 'tema teste',
    //   Local: 'local teste'
    // }

  // public eventos: any = [
  //   {
  //     Tema: 'tema teste',
  //     Local: 'local teste'
  //   },
  //   {
  //     Tema: 'tema teste1',
  //     Local: 'local teste1'
  //   },
  //   {
  //     Tema: 'tema teste2',
  //     Local: 'local teste2'
  //   }
  // ]

  public eventos: any = [];
  public eventosFiltrados: any = [];

  widthImg: number = 150; //não precisa por o tipo
  marginImg: number = 2;
  showImg: boolean = true;
  private _filtroLista: string = '';

  public get filtroLista():string {
    return this._filtroLista
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  filtrarEventos(filtrarPor: string): any{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local: string }) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEventos();
  }

  alterarImagem(){
    this.showImg = !this.showImg;

  }

  public getEventos(): void {
    this.http.get('https://localhost:5001/api/eventos').subscribe(
      response => {
        this.eventos = response;
        this.eventosFiltrados = this.eventos
      },
      error => console.log(error)
    );

    // this.eventos = [
    //   {
    //     Tema: 'tema teste',
    //     Local: 'local teste'
    //   },
    //   {
    //     Tema: 'tema teste1',
    //     Local: 'local teste1'
    //   },
    //   {
    //     Tema: 'tema teste2',
    //     Local: 'local teste2'
    //   }
    // ];
  }
}
