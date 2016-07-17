using System;

namespace test
{
    using System.Data.Entity;

    public partial class TestDb : DbContext
    {
        public TestDb()
            : base("name=TestDb")
        {
        }

        public virtual DbSet<Post> Posts { get; set; }

    }

    public class Post
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }

        public Post()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}
