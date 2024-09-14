using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
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
        
        private readonly DataContext _context;
    
        public EventoController(DataContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _context.Eventos;  
        }

        [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            return _context.Eventos.FirstOrDefault(evento => evento.EventoId == id);  
        }

        // [HttpGet("{id}")]
        // public IEnumerable<Evento> GetById(int id)
        // {
        //     return _context.Eventos.Where(evento => evento.EventoId == id);  
        // }

        [HttpPost]
        public string Post()
        {
            return "teste post";
        }

        // [HttpGet]
        // public string Get()
        // {
        //     return "value";
        // }

        // [HttpGet]
        // public string Get()
        // {
        //     return "value";
        // }
    }
}
