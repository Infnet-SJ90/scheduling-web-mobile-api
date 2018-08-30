using Microsoft.AspNetCore.Http;

namespace SchedulingWebMobileApi.Context
{
    public abstract class BaseResource
    {
        private IHttpContextAccessor _httpContextAccessor = null;

        public BaseResource(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Context = new Context();
        }

        private Context _context;
        public Context Context
        {
            get
            {
                if(this._context == null)
                {
                    this._context = (Context) _httpContextAccessor.HttpContext.Items["Context"];
                }
                return this._context;
            }
            set
            {
                value.Request = _httpContextAccessor.HttpContext.Request;
                _httpContextAccessor.HttpContext.Items["Context"] = value;
            }
        }
    }
}
