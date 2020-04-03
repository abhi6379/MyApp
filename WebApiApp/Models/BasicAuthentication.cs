using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiApp.Models
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string credentials = actionContext.Request.Headers.Authorization.Parameter;

            string decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
            string[] val = decodedCredentials.Split(':');
            string username = val[0];
            string password = val[1];
            if (Services.Login(username, password))
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "You are not authorised");
            }
        }
    }
}