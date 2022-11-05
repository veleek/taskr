using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using taskr.Shared;

namespace taskr.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly Supabase.Client _client;
    private readonly ILogger<AuthController> _logger;

    public AuthController(Supabase.Client client, ILogger<AuthController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpGet("signin")]
    public async Task<ActionResult> SignIn()
    {
        string url = await this._client.Auth.SignIn(Supabase.Gotrue.Client.Provider.Github, "user:email");
        string callback = "http://localhost:8080/auth/callback";
        url += $"&redirect_to={HttpUtility.UrlEncode(callback)}";
        return this.Redirect(url);
    }

    [HttpGet("signout")]
    public new async Task<ActionResult> SignOut()
    {
        await this._client.Auth.SignOut();
        return this.Redirect("/");
    }

    [HttpGet("callback")]
    public async Task<string> Callback()
    {
        var u = new Uri(this.Request.GetEncodedUrl());
        this._logger.LogWarning("Finally got a callback!" + u);
        var session = await this._client.Auth.GetSessionFromUrl(u);
        this._logger.LogWarning("A user id: " + session.User.Id);
        return "Successfully logged in!";
    }
}
