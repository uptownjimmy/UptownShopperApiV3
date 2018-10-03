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
    }
}