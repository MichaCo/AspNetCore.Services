using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DataService.Controllers
{
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        private static Random Rnd = new Random();
        private static readonly Dictionary<string, string> _values = new Dictionary<string, string>()
        {
            {  "a", "Value " + Rnd.Next(10, 1000) },
            {  "b", "Value " + Rnd.Next(10, 1000) },
            {  "c", "Value " + Rnd.Next(10, 1000) },
            {  "d", "Value " + Rnd.Next(10, 1000) },
            {  "e", "Value " + Rnd.Next(10, 1000) },
            {  "f", "Value " + Rnd.Next(10, 1000) },
            {  "g", "Value " + Rnd.Next(10, 1000) },
        };
        
        [HttpGet("")]
        public IActionResult Get()
        {
            return Json(_values);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (_values.TryGetValue(id, out string value))
            {
                return Json(value);
            }

            return NotFound();
        }
        
        [HttpPost("{id}")]
        public bool Post(string id, [FromBody]string value)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (_values.ContainsKey(id))
            {
                return false;
            }

            _values.Add(id, value);

            return true;
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]string value)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (_values.ContainsKey(id))
            {
                _values[id] = value;
                return Ok();
            }
            
            return NotFound();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (_values.ContainsKey(id))
            {
                _values.Remove(id);
                return Ok();
            }

            return NotFound();
        }
    }
}