using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AspNetBlog.Common.HttpContextUser;

public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _accessor;
    private readonly ILogger<AspNetUser> _logger;

    public AspNetUser(IHttpContextAccessor accessor, ILogger<AspNetUser> logger)
    {
        _accessor = accessor;
        _logger = logger;
    }

    public string Name => GetName();

    // 获取用户名
    private string GetName()
    {
        if (IsAuthenticated() && _accessor.HttpContext.User.Identity.Name.IsNotEmptyOrNull())
        {
            return _accessor.HttpContext.User.Identity.Name;
        }
        else
        {
            // TODO: (else function)
        }

        return "";
    }
    // 获取用户 ID
    public long ID => GetClaimValueByType("jti").FirstOrDefault().ObjToLong();
    // 获取租户 ID
    public long TenantId => GetClaimValueByType("TenantId").FirstOrDefault().ObjToLong();

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    // 获取用户的令牌
    public string GetToken()
    {
        var token = _accessor.HttpContext?.Request?.Headers["Authorization"].ObjToString().Replace("Bearer ", "");
        if (!token.IsNullOrEmpty())
        {
            return token;
        }

        return token;
    }

    public List<string> GetUserInfoFromToken(string ClaimType)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var token = "";

        token = GetToken();
        // token 校验
        if (token.IsNotEmptyOrNull() && jwtHandler.CanReadToken(token))
        {
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);

            return (from item in jwtToken.Claims
                where item.Type == ClaimType
                select item.Value).ToList();
        }

        return new List<string>() { };
    }

    public IEnumerable<Claim> GetClaimsIdentity()
    {
        if (_accessor.HttpContext == null) return ArraySegment<Claim>.Empty;

        if (!IsAuthenticated()) return GetClaimsIdentity(GetToken());

        var claims = _accessor.HttpContext.User.Claims.ToList();
        var headers = _accessor.HttpContext.Request.Headers;
        foreach (var header in headers)
        {
            claims.Add(new Claim(header.Key, header.Value));
        }

        return claims;
    }

    public IEnumerable<Claim> GetClaimsIdentity(string token)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        // token校验
        if (token.IsNotEmptyOrNull() && jwtHandler.CanReadToken(token))
        {
            var jwtToken = jwtHandler.ReadJwtToken(token);

            return jwtToken.Claims;
        }

        return new List<Claim>();
    }

    public List<string> GetClaimValueByType(string ClaimType)
    {
        return (from item in GetClaimsIdentity()
            where item.Type == ClaimType
            select item.Value).ToList();
    }

}