using System.Threading.Tasks;

namespace Slackers.Services.Repository.MongoDb
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}