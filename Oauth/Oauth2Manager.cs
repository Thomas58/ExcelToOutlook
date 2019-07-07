using Oauth.Wrappers;
using RestSharp;
using System;
using System.Net;

namespace Oauth
{
    public class Oauth2Manager
    {
        private Oauth2Provider Provider;
        private RestClient Client;
        private Oauth2AccessToken AccessToken;
        public bool Authorized => AccessToken != null;

        public Oauth2Manager(Oauth2Provider provider)
        {
            Provider = provider;
            Client = new RestClient();
            Client.BaseUrl = new Uri(Provider.TokenEndpoint);
        }

        public bool SendRequest(Request request)
        {
            RestRequest Request = TranslateRequest(request);
            IRestResponse Response = Client.Execute(Request);
            return TranslateResponse(Response);
        }

        public T SendRequest<T>(Request request) where T : new()
        {
            RestRequest Request = TranslateRequest(request);
            IRestResponse<T> Response = Client.Execute<T>(Request);
            if (TranslateResponse(Response))
                return Response.Data;
            else
                return default(T);
        }

        private RestRequest TranslateRequest(Request request)
        {
            RestRequest Request;
            if (request.Ressource != null)
                Request = new RestRequest(request.Ressource, request.Method.Convert());
            else
                Request = new RestRequest(request.Method.Convert());
            foreach (HttpHeader header in request.Headers)
                Request.AddHeader(header.Name, header.Value);
            foreach (Parameter parameter in request.Parameters)
                Request.AddParameter(parameter);
            if (request.Body != null)
                Request.AddJsonBody(request.Body);
            return Request;
        }

        private bool TranslateResponse(IRestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.NonAuthoritativeInformation:
                    return true;
                case HttpStatusCode.Accepted:
                case HttpStatusCode.NoContent:
                case HttpStatusCode.ResetContent:
                case HttpStatusCode.PartialContent:
                    return false;
                default:
                    Exception exception;
                    if (response.ErrorException != null)
                    {
                        exception = new Exception(response.StatusDescription, response.ErrorException);

                    }
                    else
                    {
                        exception = new Exception(response.StatusDescription);
                    }
                    exception.Data.Add("Response", response);
                    throw exception;
            }
        }

        public Uri AuthorizationWebLink => Provider.AuthorizationWebLink;

        public void EnterAuthorizationCode(string authorizationCode)
        {
            var Request = new RestRequest(Method.POST);
            Request.AddHeader("Content-Type", "application/json");
            Request.AddParameter("client_id", Provider.ClientId, "application/x-www-form-urlencoded", ParameterType.GetOrPost);
            Request.AddParameter("client_secret", Provider.ClientSecret, "application/x-www-form-urlencoded", ParameterType.GetOrPost);
            Request.AddParameter("redirect_uri", Provider.RedirectUri + Provider.RedirectPath, "application/x-www-form-urlencoded", ParameterType.GetOrPost);
            Request.AddParameter("code", authorizationCode, "application/x-www-form-urlencoded", ParameterType.GetOrPost);
            Request.AddParameter("grant_type", "authorization_code", "application/x-www-form-urlencoded", ParameterType.GetOrPost);

            IRestResponse<Oauth2AccessToken> Response = Client.Execute<Oauth2AccessToken>(Request);
            switch (Response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                    break;
                default:
                    Exception exception;
                    if (Response.ErrorException != null)
                    {
                        exception = new Exception(Response.StatusDescription, Response.ErrorException);

                    }
                    else
                    {
                        exception = new Exception(Response.StatusDescription);
                    }
                    exception.Data.Add("ResponseStatus", Response.ResponseStatus);
                    exception.Data.Add("StatusCode", Response.StatusCode);
                    throw exception;
            }

            AccessToken = Response.Data.Init();
            Client.BaseUrl = new Uri(Provider.Host);
            Client.AddDefaultHeader("Accept", "application/json");
            Client.AddDefaultHeader("Authorization", AccessToken.token_type + " " + AccessToken.access_token);
            
        }

        public void AddDefaultHeader(string name, string value)
        {
            Client.AddDefaultHeader(name, value);
        }
    }
}