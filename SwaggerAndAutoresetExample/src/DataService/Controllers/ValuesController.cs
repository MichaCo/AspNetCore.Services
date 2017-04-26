using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DataService.Controllers
{
    public class ValueModel
    {
        internal static ValueModel Create(int id)
        {
            return new ValueModel()
            {
                Id = id,
                Value = $"Value {id}",
                LastModified = DateTime.UtcNow
            };
        }

        public int Id { get; set; }

        public string Value { get; set; }

        public DateTime LastModified { get; set; }
    }

    [Route("[controller]")]
    public class ValuesController : Controller
    {
        private static object valueLock = new object();
        private static readonly List<ValueModel> _values = new List<ValueModel>()
        {
            ValueModel.Create(22),
            ValueModel.Create(23),
            ValueModel.Create(55),
            ValueModel.Create(112),
            ValueModel.Create(576),
        };

        [HttpGet("")]
        public IActionResult Get()
        {
            return Json(_values);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var value = _values.FirstOrDefault(p => p.Id == id);
            if (value == null)
            {
                return NotFound();
            }

            return Json(value);
        }

        [HttpPost("")]
        public void Post([FromBody]string value)
        {
            lock (valueLock)
            {
                var id = _values.Max(p => p.Id) + 1;

                _values.Add(new ValueModel()
                {
                    Id = id,
                    Value = value,
                    LastModified = DateTime.UtcNow
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            var existing = _values.FirstOrDefault(p => p.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.LastModified = DateTime.UtcNow;
            existing.Value = value;

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _values.FirstOrDefault(p => p.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            if (_values.Remove(existing))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}