using Dapper;
using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Context;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Domain;
using System;
using System.Data;

namespace SchedulingWebMobileApi.Core.Repository
{
    public class AuthRepository : BaseResource, IAuthRepository
    {
        private readonly IDbConnection _connection;

        public AuthRepository(IHttpContextAccessor context, IDbConnection connection) : base(context)
        {
            _connection = connection;
        }

        public Guid Authentication(Authentication authentication)
        {
            try
            {
                _connection.Open();
                var citezenKey = _connection.QueryFirstOrDefault<Guid>("SELECT CitezenKey FROM Citezen WHERE (Email = @Email OR Cpf = @Cpf) and Senha = @Senha LIMIT 1", new { Email = authentication.Email, Senha = authentication.Senha, Cpf = authentication.Cpf });

                if (citezenKey != null && citezenKey != Guid.Empty)
                {
                    var token = Guid.NewGuid();
                    _connection.Execute("INSERT INTO Authentication (CitezenKey, Token, AuthenticationType, Date) VALUES (@CitezenKey, @Token, @AuthenticationType, @Date)", new { CitezenKey = citezenKey, Token = token, AuthenticationType = authentication.AuthenticationType, Date = DateTime.Now });

                    return token;
                }

                return Guid.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool IsTokenValid(Guid token)
        {
            try
            {
                _connection.Open();
                var result = _connection.QueryFirstOrDefault("select a.AuthenticationType, c.* From Authentication a           inner join Citezen c on c.CitezenKey = a.CitezenKey                                                  where a.Token = @Token                                                                               ORDER BY a.Date DESC                                                                                 LIMIT 1", new { Token = token});

                if (result == null)
                    return false;

                var authenticationType = (AuthenticationType)Enum.Parse(typeof(AuthenticationType), result.AuthenticationType);

                switch (authenticationType)
                {
                    case AuthenticationType.LOGIN:
                        this.Context.Citezen = new Citezen()
                        {
                            CitezenKey = result.CitezenKey,
                            Cpf = result.Cpf,
                            Email = result.Email,
                            Nome = result.Nome
                        };
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
