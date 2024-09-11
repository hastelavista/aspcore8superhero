using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet8.Data;
using SuperHeroAPI_DotNet8.Entities;

namespace SuperHeroAPI_DotNet8.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _Context;

        public SuperHeroController(DataContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes() 
        {
            var heroes = await _Context.SuperHeroes.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SuperHero>> GetById(int id)
        {
            var hero = await _Context.SuperHeroes.FindAsync(id);
            if(hero is null)
                return NotFound("Hero Not Found!");
            else

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
           _Context.SuperHeroes.Add(hero);
            await _Context.SaveChangesAsync();

            return Ok(await _Context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updatedHero)
        {

            var dbHero = await _Context.SuperHeroes.FindAsync(updatedHero.Id);
            if (dbHero is null)
                return NotFound("Hero Not Found!");
            
            dbHero.Name = updatedHero.Name;
            dbHero.FirstName = updatedHero.FirstName;
            dbHero.LastName = updatedHero.LastName;
            dbHero.Address = updatedHero.Address;

            await _Context.SaveChangesAsync();

            return Ok(await _Context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await _Context.SuperHeroes.FindAsync(id);
            if (dbHero is null)
                return NotFound("Hero Not Found!");
  
            _Context.SuperHeroes.Remove(dbHero);
            await _Context.SaveChangesAsync();

            return Ok(await _Context.SuperHeroes.ToListAsync());
        }




    }

}
