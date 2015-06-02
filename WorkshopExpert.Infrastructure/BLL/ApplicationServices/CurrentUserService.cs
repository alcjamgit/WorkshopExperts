using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WorkshopExpert.Core.ApplicationServices;


namespace WorkshopExpert.Infrastructure.BLL.ApplicationServices
{
    /// <summary>
    /// Container for HttpContextBase.User to be able to make controllers that uses User.Identity.GetUserId() testable
    /// http://stackoverflow.com/questions/3027264/mocking-user-identity-in-asp-net-mvc
    /// http://stackoverflow.com/questions/8246709/unity-equivalent-for-ninjects-bind-tomethod-of-iprincipal-iidentity
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IIdentity identity)
        {
            IsAuthenticated = identity.IsAuthenticated;
            UserName = identity.Name;
            UserID = identity.GetUserId();
        }
        //for mocking
        public CurrentUserService() { }

        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public virtual string UserID { get; set; }
    }
}
