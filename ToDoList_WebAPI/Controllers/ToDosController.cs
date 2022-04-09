using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList_WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ToDoContext _context;
        public ToDosController (ToDoContext context)
        {
            _context = context;
        }

        // GET: api/<ToDosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDos()
        {
            return await _context.ToDos.ToListAsync();
        }

        // GET api/<ToDosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetTodo(int id)
        {
            var todo = await _context.ToDos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return todo;

        }

        // POST api/<ToDosController>
        [HttpPost]
        public async Task<ActionResult<ToDo>> PostTodo(ToDo todo)
        {
            _context.ToDos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTodo", new { id = todo.ID }, todo);
        }

        // PUT api/<ToDosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDo>> PutTodo(int id, ToDo todo)
        {
            if (id != todo.ID)
            {
                return BadRequest();
            }
            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return todo;
        }


        // DELETE api/<ToDosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo(int id)
        {
            var todo = await _context.ToDos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            _context.ToDos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return _context.ToDos.Any(e => e.ID == id);
        }
    }
}
