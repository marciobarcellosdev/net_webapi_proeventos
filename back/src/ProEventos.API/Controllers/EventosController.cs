﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Domain;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        // public IEnumerable<Evento> _evento = new Evento[]{
        //     new Evento(){
        //             EventoId = 1,
        //             Tema = "tema",
        //             Local = "local",
        //             Lote = "primeiro",
        //             QtdPessoas = 25,
        //             DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
        //             Image = "foto.aff"
        //         },
        //         new Evento(){
        //             EventoId = 2,
        //             Tema = "tema",
        //             Local = "local",
        //             Lote = "primeiro",
        //             QtdPessoas = 25,
        //             DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
        //             Image = "foto.aff"
        //         }
        // };
        
        //services.AddScoped<IEventoService, EventoService>(); -- Startup.cs
        public readonly IEventoService _eventoService;
    
        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
            //_context = context;
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("Nenhum evento encontrato");
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }  
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if(evento == null) return NotFound("Evento por Id não encontrato");
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar evento. Erro: {ex.Message}");
            }    
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if(evento == null) return NotFound("Eventos por tema não encontrado");
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }    
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if(evento == null) return BadRequest("Erro ao tentar adicionar evento.");
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar evento. Erro: {ex.Message}");
            }    
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if(evento == null) return BadRequest("Erro ao tentar atualizar evento.");
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar evento. Erro: {ex.Message}");
            }    
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _eventoService.DeleteEvento(id) ? Ok("Deletado") : BadRequest("Evento não deletado");
                // if(await _eventoService.DeleteEvento(id)){
                //     return Ok("Deletado");
                // }
                // else{
                //     return BadRequest("Evento não deletado");
                // }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar evento. Erro: {ex.Message}");
            }    
        }
    }
}
