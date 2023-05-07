using Microsoft.EntityFrameworkCore;

namespace WebManytoManyDBRepository.DBManagement
{
    //public class MovieDBService : IDataAccessService, IDisposable
    //{
    //    protected readonly MovieContext _context;

    //    private Dictionary<string, object> _repos;

    //    public MovieDBService(IDbContextFactory<MovieContext> factory)
    //    {
    //        _context = factory.CreateDbContext();

    //        _repos = new Dictionary<string, object>();
    //    }

    //    public async Task<int> CompleteAsync()
    //    {
    //        var numOfChanges = await _context.SaveChangesAsync();

    //        if (_context.ChangeTracker != null)
    //        {
    //            _context.ChangeTracker.Clear();
    //        }

    //        return numOfChanges;
    //    }

    //    //public       

    //    private bool disposed = false;

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!this.disposed)
    //        {
    //            if (disposing)
    //            {
    //                _context.Dispose();
    //            }
    //        }
    //        this.disposed = true;
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }
    //}
}
