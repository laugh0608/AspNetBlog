using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetBlog.Model.Vo;
using Microsoft.IdentityModel.Tokens;

namespace AspNetBlog.Extension.ServiceExtensions;

/// <summary>
/// JWTToken 生成类
/// </summary>
public class JwtToken
{
    /// <summary>
    /// 获取基于 JWT 的 Token
    /// </summary>
    /// <param name="claims">需要在登陆的时候配置</param>
    /// <param name="permissionRequirement">在 startup 中定义的参数</param>
    /// <returns></returns>
    public static TokenInfoViewModel BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement)
    {
        var now = DateTime.Now;
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfsdfsrty45634kkhllghtdgdfss345t678fs"));
        // 实例化 JwtSecurityToken
        var jwt = new JwtSecurityToken(
            issuer: "Blog.Core",
            audience: "wr",
            claims: claims,
            notBefore: now,
            expires: DateTime.Now.AddSeconds(3600),
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        );
        // 生成 Token
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        // 打包返回前台
        var responseJson = new TokenInfoViewModel
        {
            success = true,
            token = encodedJwt,
            expires_in = 3600,
            token_type = "Bearer"
        };
        return responseJson;
    }
}