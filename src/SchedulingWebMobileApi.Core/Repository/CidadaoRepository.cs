using Dapper;
using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Domain;
using System;
using System.Data;

namespace SchedulingWebMobileApi.Core.Repository
{
    public class CidadaoRepository : RepositoryBase<Cidadao>, ICidadaoRepository
    {
        public CidadaoRepository(IHttpContextAccessor context, IDbConnection connection) : base(connection, context) { }

        public override bool Delete(Guid key)
        {
            try
            {
                _connection.Open();
                return _connection.Execute("DELETE FROM Cidadao WHERE CidadaoKey = @CidadaoKey", new { CidadaoKey = key }) > 0;
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
                return _connection.QueryFirstOrDefault<bool>("SELECT 1 FROM Cidadao WHERE Email = @Email or CidadaoKey = @CidadaoKey or Cpf = @Cpf LIMIT 1", new { Email = cidadao.Email, CidadaoKey = cidadao.CidadaoKey, Cpf = cidadao.Cpf });
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

        public bool Exists(string email)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<bool>("SELECT 1 FROM Cidadao WHERE Email = @Email LIMIT 1", new { Email = email });
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
                return _connection.QueryFirstOrDefault<Cidadao>("SELECT * FROM Cidadao WHERE CidadaoKey = @CidadaoKey LIMIT 1", new { CidadaoKey = key });
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

        public override Cidadao Insert(Cidadao entity)
        {
            try
            {
                _connection.Open();
                _connection.Execute("INSERT INTO Cidadao (CidadaoKey, Nome, Email, Cpf, Senha) VALUES (@CidadaoKey, @Nome, @Email, @Cpf, @Senha)", entity);
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
                _connection.Execute("UPDATE Cidadao SET Nome = @Nome, Email = @Email, Senha = @Senha where CidadaoKey = @CidadaoKey", entity);
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
