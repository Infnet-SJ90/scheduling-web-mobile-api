using Dapper;
using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Context;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace SchedulingWebMobileApi.Core.Repository
{
    public class LocalRepository : RepositoryBase<Local>, ILocalRepository
    {
        public LocalRepository(IHttpContextAccessor context, IDbConnection connection) : base(connection, context){}

        public override bool Delete(Guid key)
        {
            try
            {
                _connection.Open();
                return _connection.Execute("DELETE FROM Endereco WHERE EnderecoKey = @EnderecoKey", new { EnderecoKey = key }) > 0;
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

        public bool Exists(Local local)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<bool>("SELECT 1 FROM Endereco WHERE Cep = @Cep AND Numero = @Numero LIMIT 1", local);
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

        public override Local Get(Guid key)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<Local>("SELECT * FROM Endereco WHERE EnderecoKey = @EnderecoKey LIMIT 1", new { EnderecoKey = key });
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

        public IList<Local> Get()
        {
            try
            {
                _connection.Open();
                var cidadaoKey = this.Context.Cidadao.CidadaoKey;
                return _connection.Query<Local>("SELECT * FROM Endereco where CidadaoKey = @CidadaoKey", new { CidadaoKey = cidadaoKey}).AsList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            
        }

        public override Local Insert(Local entity) 
        {
            try
            {
                _connection.Open();
                var cidadaoKey = this.Context.Cidadao.CidadaoKey;
                _connection.Execute("INSERT INTO Endereco (EnderecoKey, Cep, Estado, Cidade, Bairro, Rua, Numero, CidadaoKey) VALUES (@EnderecoKey, @Cep, @Estado, @Cidade, @Bairro, @Rua, @Numero, @CidadaoKey)", new { EnderecoKey = entity.EnderecoKey, Cep = entity.Cep, Estado = entity.Estado, Cidade = entity.Cidade, Bairro = entity.Bairro, Rua = entity.Rua, Numero = entity.Numero, CidadaoKey = cidadaoKey});
                return entity;
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

        public override Local Update(Local entity)
        {
            throw new NotImplementedException();
        }
    }
}
