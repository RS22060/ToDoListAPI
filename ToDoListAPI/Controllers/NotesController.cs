using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI;
using ToDoListAPI.Models;

namespace ToDoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly ToDoContext _context;

        public NotesController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetToDos()
        {
          if (_context.ToDos == null)
          {
              return NotFound();
          }
            return await _context.ToDos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
          if (_context.ToDos == null)
          {
              return NotFound();
          }
            var note = await _context.ToDos.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            if (id != note.NoteId)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
          if (_context.ToDos == null)
          {
              return Problem("Entity set 'ToDoContext.ToDos'  is null.");
          }
            _context.ToDos.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.NoteId }, note);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            if (_context.ToDos == null)
            {
                return NotFound();
            }
            var note = await _context.ToDos.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.ToDos.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return (_context.ToDos?.Any(e => e.NoteId == id)).GetValueOrDefault();
        }
    }
}
