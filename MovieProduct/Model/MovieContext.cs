﻿using Microsoft.EntityFrameworkCore;

namespace MovieProduct.Model
{
    public class MovieContext:DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options):base(options)
        {
            
        }
        public DbSet<Movie> movies { get; set; }
    }
}
