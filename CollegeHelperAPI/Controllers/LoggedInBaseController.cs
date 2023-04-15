using CollegeHelperAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CollegeHelperAPI.Controllers
{
    public class LoggedInBaseController : ControllerBase
    {
        public LoggedInUser LoggedInUser
        {
            get
            {
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                LoggedInUser loggedInUser = new LoggedInUser();

                if (claimsIdentity.Name == "")
                {
                    loggedInUser = null;
                }
                else
                {
                    if (claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault() != null)
                        loggedInUser.UserID = Convert.ToInt32(claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value);
                    if (claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault() != null)
                        loggedInUser.Email = Convert.ToString(claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value);
                    if (claimsIdentity.Claims.Where(c => c.Type == "FirstName").FirstOrDefault() != null)
                        loggedInUser.FirstName = Convert.ToString(claimsIdentity.Claims.Where(c => c.Type == "FirstName").FirstOrDefault().Value);
                    if (claimsIdentity.Claims.Where(c => c.Type == "LastName").FirstOrDefault() != null)
                        loggedInUser.LastName = Convert.ToString(claimsIdentity.Claims.Where(c => c.Type == "LastName").FirstOrDefault().Value);
                }
                return loggedInUser;
            }
        }

        protected override void ExecuteCore()
        {
            throw new NotImplementedException();
        }
    }
}
