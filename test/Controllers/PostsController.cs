using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace test.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostsController : ApiController
    {
        private TestDb db = new TestDb();

        [HttpGet]
        [Route("getAll")]
        public IQueryable<Post> GetPosts()
        {
            return db.Posts;
        }

        [HttpGet]
        [Route("find")]
        public IHttpActionResult GetPost([FromUri]Guid id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }
        [HttpPost]
        [Route("save")]
        public IHttpActionResult PostPost([FromBody]PostReq data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var post = new Post() { Text = data.Text};
            db.Posts.Add(post);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PostExists(post.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(post);
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(Guid id)
        {
            return db.Posts.Count(e => e.Id == id) > 0;
        }
    }

    public class PostReq
    {
        public string Text { get; set; }
    }
}