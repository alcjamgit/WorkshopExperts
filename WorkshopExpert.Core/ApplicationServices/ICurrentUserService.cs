using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopExpert.Core.ApplicationServices
{
    public interface ICurrentUserService
    {
        string UserID { get; set; }
        string UserName { get; set; }
        bool IsAuthenticated { get; set; }
    }
}
