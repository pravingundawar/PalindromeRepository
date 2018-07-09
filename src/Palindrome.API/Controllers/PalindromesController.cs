using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Palindromes.API.Infrastructure;
using Palindromes.API.Model;
using Palindromes.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Palindromes.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PalindromesController : ControllerBase
    {
        private readonly PalindromesContext _palindromesContext;
        private readonly PalindromesSettings _settings;

        public PalindromesController(PalindromesContext context, IOptionsSnapshot<PalindromesSettings> settings)
        {
            _palindromesContext = context ?? throw new ArgumentNullException(nameof(context));
            _settings = settings.Value;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ItemsViewModel<Palindrome>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Palindrome>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery]int pageSize = 100, [FromQuery]int pageIndex = 0)
        {
            var totalItems = await _palindromesContext.Palindromes.LongCountAsync();

            var itemsOnPage = await _palindromesContext.Palindromes
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedDate)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            var model = new ItemsViewModel<Palindrome>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        [HttpGet]
        [Route("/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Palindrome), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _palindromesContext.Palindromes.SingleOrDefaultAsync(ci => ci.Id == id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody]Palindrome palindrome)
        {
            bool exists = await _palindromesContext.Palindromes.AnyAsync(p => p.Text.ToLower() == palindrome.Text);
            if (exists)
            {
                return BadRequest("Palindrome already exists.");
            }
            var item = new Palindrome
            {
                Text = palindrome.Text,
                CreatedDate = DateTime.Now
            };

            _palindromesContext.Palindromes.Add(item);
            await _palindromesContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { Id = item.Id}, null);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var palindrome = _palindromesContext.Palindromes.SingleOrDefault(x => x.Id == id);

            if (palindrome == null)
            {
                return NotFound();
            }

            _palindromesContext.Palindromes.Remove(palindrome);

            await _palindromesContext.SaveChangesAsync();

            return NoContent();
        }

    }
}

