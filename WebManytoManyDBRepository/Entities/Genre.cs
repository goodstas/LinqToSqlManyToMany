namespace WebManytoManyDBRepository.Entities
{
    public class Genre : IEntity<long>, IMergeable<Genre>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<Movie> Movies { get; set; }

        public bool HasIdenticalId(long id)
        {
            return this.Id == id;
        }

        public void Merge(Genre entity)
        {
            this.Name   = entity.Name;

            Dictionary<long, Movie> currentMovies = new System.Collections.Generic.Dictionary<long, Movie>();
            Dictionary<long, Movie> updatedMovies = new System.Collections.Generic.Dictionary<long, Movie>();

            this.Movies.ForEach(genre => currentMovies.Add(genre.Id, genre));
            entity.Movies.ForEach(genre => updatedMovies.Add(genre.Id, genre));

            this.Movies.Clear();

            foreach (var genre in updatedMovies)
            {
                if (currentMovies.ContainsKey(genre.Key))
                {
                    this.Movies.Add(currentMovies[genre.Key]);
                }
                else
                {
                    this.Movies.Add(genre.Value);
                }
            }
        }
    }
}
