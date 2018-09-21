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
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(IHttpContextAccessor context, IDbConnection connection) : base(connection, context){}

        public override bool Delete(Guid key)
        {
            try
            {
                _connection.Open();
                return _connection.Execute("DELETE FROM Address WHERE AddressKey = @AddressKey", new { AddressKey = key }) > 0;
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

        public bool Exists(Address address)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<bool>("SELECT 1 FROM Address WHERE Cep = @Cep AND Numero = @Numero LIMIT 1", address);
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

        public override Address Get(Guid key)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<Address>("SELECT * FROM Address WHERE AddressKey = @AddressKey LIMIT 1", new { AddressKey = key });
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

        public IList<Address> Get()
        {
            try
            {
                _connection.Open();
                var citezenKey = this.Context.Citezen.CitezenKey;
                return _connection.Query<Address>("SELECT * FROM Address where CitezenKey = @CitezenKey", new { CitezenKey = citezenKey}).AsList();
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

        public override Address Insert(Address entity) 
        {
            try
            {
                _connection.Open();
                var citezenKey = this.Context.Citezen.CitezenKey;
                _connection.Execute("INSERT INTO Address (AddressKey, Cep, Estado, Cidade, Bairro, Rua, Numero, CitezenKey) VALUES (@AddressKey, @Cep, @Estado, @Cidade, @Bairro, @Rua, @Numero, @CitezenKey)", new { AddressKey = entity.AddressKey, Cep = entity.Cep, Estado = entity.Estado, Cidade = entity.Cidade, Bairro = entity.Bairro, Rua = entity.Rua, Numero = entity.Numero, CitezenKey = citezenKey});
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

        public override Address Update(Address entity)
        {
            throw new NotImplementedException();
        }
    }
}
