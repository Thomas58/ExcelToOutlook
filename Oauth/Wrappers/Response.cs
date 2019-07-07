using System.Collections.Generic;

namespace Oauth.Wrappers
{
    public class Response<T>
    {
        public List<T> value { get; set; }
    }
}
