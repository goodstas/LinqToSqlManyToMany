using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebManytoManyDBRepository.Entities
{
    public class Movie : IEntity<long>, IMergeable<Movie>
    {
        public long Id { get ; set ; }

        public string Title { get; set ; }

        public List<Genre> Genres { get; set ; }

        public bool HasIdenticalId(long id)
        {
            return this.Id == id ;
        }

        public void Merge(Movie entity)
        {
            this.Title  = entity.Title;

            Dictionary<long, Genre> currentGenres = new System.Collections.Generic.Dictionary<long, Genre>();
            Dictionary<long, Genre> updatedGenres = new System.Collections.Generic.Dictionary<long, Genre>();

            this.Genres.ForEach(genre => currentGenres.Add(genre.Id, genre));
            entity.Genres.ForEach(genre => updatedGenres.Add(genre.Id, genre));

            this.Genres.Clear();

            foreach (var genre in updatedGenres)
            {
                if (currentGenres.ContainsKey(genre.Key))
                {
                    this.Genres.Add(currentGenres[genre.Key]);
                }
                else
                {
                    this.Genres.Add(genre.Value);
                }
            }
        }
    }
}
