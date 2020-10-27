﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using FFBStats.Web.Extensions;
using YahooFantasyWrapper.Client;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FFBStats.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        public AuthenticationController(IYahooAuthClient authClient)
        {
            AuthClient = authClient;
        }

        public IYahooAuthClient AuthClient { get; }

        [HttpGet("~/signin")]
        public async Task<IActionResult> SignIn()
        {
            // Instruct the middleware corresponding to the requested external identity
            // provider to redirect the user agent to its own authorization endpoint.
            // Note: the authenticationScheme parameter must match the value configured in Startup.cs
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "Yahoo");
        }

        [HttpGet("~/signout")]
        [HttpPost("~/signout")]
        public IActionResult SignOut()
        {
            // Instruct the cookies middleware to delete the local cookie created
            // when the user agent is redirected from the external identity provider
            // after a successful authentication flow (e.g Google or Facebook).
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
