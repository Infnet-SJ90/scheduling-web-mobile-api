using Dapper;
using SchedulingWebMobileApi.Core.Entities;
using SchedulingWebMobileApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SchedulingWebMobileApi.Core.Repository
{
    public class CidadaoRepository : RepositoryBase<Cidadao>, ICidadaoRepository
    {
        public CidadaoRepository(IDbConnection connection) : base(connection) { }

        public Guid Authentication(Authentication authentication)
        {
            try
            {
                _connection.Open();
                var cidadao = _connection.QueryFirstOrDefault<Cidadao>("SELECT * FROM Cidadao WHERE (Email = @Email OR Cpf = @Cpf) and Senha = @Senha LIMIT 1", new { Email = authentication.Email, Senha = authentication.Senha, Cpf = authentication.Cpf });

                if (cidadao != null)
                {
                    var token = Guid.NewGuid();
                    _connection.Execute("INSERT INTO Authentication (CidadaoKey, Token, AuhenticationType, Date) VALUES (@CidadaoKey, @Token, @AuhenticationType, @Date)", new { CidadaoKey = cidadao.CidadaoKey, Token = token, AuthenticationType = authentication.AuthenticationType, Date = DateTime.Now });

                    return token;
                }

                return Guid.Empty;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                _connection.Close();
            }
        }

        public override bool Delete(Guid key)
        {
            try
            {
                _connection.Open();
                return _connection.Execute("DELETE FROM CIDADAO WHERE CidadaoKey = @CidadaoKey", new { CidadaoKey = key }) > 0;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool Exists(Cidadao cidadao)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<bool>("SELECT 1 FROM CIDADAO WHERE Email = @Email or CidadaoKey = @CidadaoKey or Cpf = @Cpf LIMIT 1", new { Email = cidadao.Email, CidadaoKey = cidadao.CidadaoKey, Cpf = cidadao.Cpf });
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool Exists(string email)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<bool>("SELECT 1 FROM CIDADAO WHERE Email = @Email LIMIT 1", new { Email = email });
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                _connection.Close();
            }
        }

        public override Cidadao Get(Guid key)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<Cidadao>("SELECT * FROM CIDADAO WHERE CidadaoKey = @CidadaoKey LIMIT 1", new { UserKey = key });
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                _connection.Close();
            }
        }

        public override Cidadao Insert(Cidadao entity)
        {
            try
            {
                _connection.Open();
                _connection.Execute("INSERT INTO CIDADAO (CidadaoKey, Nome, Email, Cpf, Senha) VALUES (@CidadaoKey, @Nome, @Email, @Cpf, @Senha)", entity);
                return entity;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                _connection.Close();
            }
        }

        public override Cidadao Update(Cidadao entity)
        {
            try
            {
                _connection.Open();
                _connection.Execute("UPDATE CIDADAO SET Nome = @Nome, Email = @Email, Senha = @Senha where CidadaoKey = @CidadaoKey", entity);
                return entity;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
