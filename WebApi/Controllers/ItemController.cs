using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemContext _context;
        
        public ItemController(ItemContext context)
        {
            _context = context;
            
            if (!_context.Items.Any())
            {
                _context.Items.Add(new Item { Name = "Item1" }); 
                _context.SaveChanges();
            }
        }
        
        [HttpGet] 
        public ActionResult<List<Item>> GetAll() 
        {     
            return _context.Items.ToList(); 
        } 
 
        [HttpGet("{id}", Name = "GetItem")] 
        public ActionResult<Item> GetById(long id) 
        {    
            var item = _context.Items.Find(id);  
            
            if (item == null)    
            {         
                return NotFound();     
            }     
            return item; 
        }
        
        [HttpPost]
        public IActionResult Create(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetItem", new { id = item.Id }, item);
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(long id, Item item)
        {
            var itemToUpdate = _context.Items.Find(id);
            if (itemToUpdate == null)
            {
                return NotFound();
            }

            itemToUpdate.Name = item.Name;
            itemToUpdate.Item_Type = item.Item_Type;
            itemToUpdate.Active = item.Active;
            itemToUpdate.Notes = item.Notes;

            _context.Items.Update(itemToUpdate);
            _context.SaveChanges();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Items.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Items.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}