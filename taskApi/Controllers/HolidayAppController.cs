using System.Web.Http;

namespace taskApi.Controllers
{

    [Authorize]
    public abstract class HolidayAppController : ApiController
    {

    }
}