using Bookmarks.Api.Models;

namespace Bookmarks.Api.Services
{
    public interface IDataBaseServices
    {
        bool Add(UrlList url);
        UrlList Get(string name);
        bool Existing(string name);
    }
}
