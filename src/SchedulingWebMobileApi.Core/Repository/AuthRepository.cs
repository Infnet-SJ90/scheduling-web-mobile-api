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
                var cidadaoKey = _connection.QueryFirstOrDefault<Guid>("SELECT CidadaoKey FROM Cidadao WHERE (Email = @Email OR Cpf = @Cpf) and Senha = @Senha LIMIT 1", new { Email = authentication.Email, Senha = authentication.Senha, Cpf = authentication.Cpf });

                if (cidadaoKey != null && cidadaoKey != Guid.Empty)
                {
                    var token = Guid.NewGuid();
                    _connection.Execute("INSERT INTO Authentication (CidadaoKey, Token, AuthenticationType, Date) VALUES (@CidadaoKey, @Token, @AuthenticationType, @Date)", new { CidadaoKey = cidadaoKey, Token = token, AuthenticationType = authentication.AuthenticationType, Date = DateTime.Now });

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
                var result = _connection.QueryFirstOrDefault("select a.AuthenticationType, c.* From Authentication a           inner join Cidadao c on c.CidadaoKey = a.CidadaoKey                                                  where a.Token = @Token                                                                               ORDER BY a.Date DESC                                                                                 LIMIT 1", new { Token = token});

                if (result == null)
                    return false;

                var authenticationType = (AuthenticationType)Enum.Parse(typeof(AuthenticationType), result.AuthenticationType);

                switch (authenticationType)
                {
                    case AuthenticationType.LOGIN:
                        this.Context.Cidadao = new Cidadao()
                        {
                            CidadaoKey = result.CidadaoKey,
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
