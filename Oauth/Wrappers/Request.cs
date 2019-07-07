using RestSharp;
using System.Collections.Generic;

namespace Oauth.Wrappers
{
    public enum Method2 { GET, POST, PUT, PATCH, DELETE }
    public static class Extension
    {
        public static RestSharp.Method Convert(this Method2 method)
        {
            switch (method)
            {
                case Method2.GET:
                    return RestSharp.Method.GET;
                case Method2.POST:
                    return RestSharp.Method.POST;
                case Method2.PUT:
                    return RestSharp.Method.PUT;
                case Method2.PATCH:
                    return RestSharp.Method.PATCH;
                case Method2.DELETE:
                    return RestSharp.Method.DELETE;
                default:
                    return RestSharp.Method.GET;
            }
        }
    }

    public class Request
    {
        public Method2 Method { get; set; }
        public string Ressource { get; set; }
        public List<Parameter> Parameters { get; private set; } = new List<Parameter>();
        public List<HttpHeader> Headers { get; private set; }
        public object Body { get; set; }

        public Request(Method2 method)
        {
            Method = method;
        }

        public Request(string ressource, Method2 method) : this(method)
        {
            Ressource = ressource;
        }

        public void AddParameter(string name, object value, string contentType, ParameterType type)
        {
            Parameters.Add(new Parameter() { ContentType = contentType, Name = name, Type = type, Value = value });
        }

        public void AddParameter(string name, object value, ParameterType type) => AddParameter(name, value, Method == Method2.PUT ? "application/x-www-form-urlencoded" : null, type);
        public void AddParameter(string name, object value) => AddParameter(name, value, Method == Method2.PUT ? "application/x-www-form-urlencoded" : null, Method == Method2.PUT ? ParameterType.GetOrPost : ParameterType.QueryString);

        public void AddHeader(string name, string value)
        {
            if (Headers == null)
                Headers = new List<HttpHeader>();
            Headers.Add(new HttpHeader() { Name = name, Value = value });
        }

        public void AddBody(object obj)
        {
            Body = obj;
        }
    }
}
