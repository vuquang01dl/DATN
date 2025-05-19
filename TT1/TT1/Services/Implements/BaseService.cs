using TT1.AppDbContexts;

namespace TT1.Services.Implements
{
    public class BaseService
    {
        public readonly AppDbContext _context;
        public BaseService()
        {
            _context = new AppDbContext();
        }

    }
}
