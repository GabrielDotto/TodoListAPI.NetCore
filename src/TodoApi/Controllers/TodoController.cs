using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{ 
    
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class TodoController : Controller
    {

        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
            //if (_context.TodoItems.Count() == 0)
            //{
            //    _context.TodoItems.Add(new TodoItem { Titulo = "tarefaAPICore" });
            //    _context.TodoItems.Add(new TodoItem { Titulo = "Enable Cors" });
            //    _context.TodoItems.Add(new TodoItem { Titulo = "Api .NET Core" });
            //    _context.TodoItems.Add(new TodoItem { Titulo = "Doidera" });
            //    _context.TodoItems.Add(new TodoItem { Titulo = "Funciona" });
            //    _context.TodoItems.Add(new TodoItem { Titulo = ":D" });
            //    _context.TodoItems.Add(new TodoItem { Titulo = "elelelelele" });
            //    _context.SaveChanges();
            //}
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTarefa")]
        public IActionResult GetById(long id)
        {
            var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if(item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if(item == null)
            {
                BadRequest();
            }

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTarefa", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id,[FromBody] TodoItem item)
        {
            if(item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if(todo == null)
            {
                return NotFound();
            }

            todo.Titulo = item.Titulo;
            todo.DataConclusao = item.DataConclusao;

            //Confirmar ....

            _context.TodoItems.Update(todo);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if(todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}

