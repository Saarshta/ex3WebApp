using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ex3WebApp.Models;

namespace ex3WebApp.Controllers
{
    public class UsersController : ApiController
    {
        private ex3WebAppContext db = new ex3WebAppContext();

        // GET: api/Users
        public IQueryable<Users> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [Route("api/Users/GetUser")]
        [HttpGet]
        //[ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> GetUser(string name, string password)
        {
            Users user = FindUser(name);
            if (user == null)
            {
                return Ok("User not found");
            }
            else if (!user.Password.Equals(password))
            {
                return Ok("Wrong password");
            }
            else
            {
                return Ok(user.Name);
            }

        }

        private Users FindUser(string name)
        {
            IQueryable<Users> listOfUsers = GetUsers();
            foreach (Users user in listOfUsers)
            {
                if (user.Name == name)
                {
                    return user;
                }
            }
            return null;
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]

        public async Task<IHttpActionResult> PutUsers(int id, Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.Id)
            {
                return BadRequest();
            }

            db.Entry(users).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        //[ResponseType(typeof(Users))]
        [Route("api/Users/PostUsers")]
        [HttpPost]
        public async Task<IHttpActionResult> PostUsers(Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (db.Users.Count(e => e.Name.Equals(users.Name)) > 0)
            {
                return Ok("Error: User already exists");
            }
            db.Users.Add(users);
            await db.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> DeleteUsers(int id)
        {
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            
            db.Users.Remove(users);
            await db.SaveChangesAsync();

            return Ok(users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}