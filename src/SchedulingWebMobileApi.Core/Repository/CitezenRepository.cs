using Dapper;
using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Domain;
using System;
using System.Data;

namespace SchedulingWebMobileApi.Core.Repository
{
    public class CitezenRepository : RepositoryBase<Citezen>, ICitezenRepository
    {
        public CitezenRepository(IHttpContextAccessor context, IDbConnection connection) : base(connection, context) { }

        public override bool Delete(Guid key)
        {
            try
            {
                _connection.Open();
                return _connection.Execute("DELETE FROM Citezen WHERE CitezenKey = @CitezenKey", new { CitezenKey = key }) > 0;
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

        public bool Exists(Citezen citezen)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<bool>("SELECT 1 FROM Citezen WHERE Email = @Email or CitezenKey = @CitezenKey or Cpf = @Cpf LIMIT 1", new { Email = citezen.Email, CitezenKey = citezen.CitezenKey, Cpf = citezen.Cpf });
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
                return _connection.QueryFirstOrDefault<bool>("SELECT 1 FROM Citezen WHERE Email = @Email LIMIT 1", new { Email = email });
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

        public override Citezen Get(Guid key)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<Citezen>("SELECT * FROM Citezen WHERE CitezenKey = @CitezenKey LIMIT 1", new { CitezenKey = key });
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

        public override Citezen Insert(Citezen entity)
        {
            try
            {
                _connection.Open();
                _connection.Execute("INSERT INTO Citezen (CitezenKey, Nome, Email, Cpf, Senha) VALUES (@CitezenKey, @Nome, @Email, @Cpf, @Senha)", entity);
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

        public override Citezen Update(Citezen entity)
        {
            try
            {
                _connection.Open();
                _connection.Execute("UPDATE Citezen SET Nome = @Nome, Email = @Email, Senha = @Senha where CitezenKey = @CitezenKey", entity);
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
