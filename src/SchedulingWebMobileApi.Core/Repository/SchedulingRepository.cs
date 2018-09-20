using Dapper;
using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Context;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace SchedulingWebMobileApi.Core.Repository
{
    public class SchedulingRepository : RepositoryBase<Scheduling>, ISchedulingRepository
    {
        public SchedulingRepository(IHttpContextAccessor context, IDbConnection connection) : base(connection, context) { }

        public override bool Delete(Guid key)
        {
            try
            {
                _connection.Open();
                return _connection.Execute("DELETE FROM Scheduling WHERE SchedulingKey = @SchedulingKey", new { SchedulingKey = key }) > 0;
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

        public bool Exists(Scheduling scheduling)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<bool>("SELECT 1 FROM Scheduling WHERE Data = @Data AND Hora = @Hora LIMIT 1", scheduling);
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

        public override Scheduling Get(Guid key)
        {
            try
            {
                _connection.Open();
                var scheduling = _connection.QueryFirstOrDefault<dynamic>("SELECT Scheduling.SchedulingKey, " +
                    "DATE_FORMAT(Scheduling.Data, '%d/%m/%Y %H:%i:%s') as Data, " +
                    "DATE_FORMAT(Scheduling.Hora, '%d/%m/%Y %H:%i:%s') as Hora, " +
                    "Scheduling.Tipo, Scheduling.Status, Address.* " +
                    "FROM Scheduling " +
                    "INNER JOIN Address on Address.enderecoKey = Scheduling.Addresskey " +
                    "WHERE SchedulingKey = @SchedulingKey LIMIT 1", new { SchedulingKey = key });

                if (scheduling != null)
                {
                    return new Scheduling()
                    {
                        SchedulingKey = scheduling.SchedulingKey,
                        Data = DateTime.Parse(scheduling.Data),
                        Hora = DateTime.Parse(scheduling.Hora),
                        Tipo = scheduling.Tipo,
                        Status = scheduling.Status,
                        Address = new Address()
                        {
                            AddressKey = scheduling.AddressKey,
                            Bairro = scheduling.Bairro,
                            Cep = scheduling.Cep,
                            Cidade = scheduling.Cidade,
                            Estado = scheduling.Estado,
                            Numero = scheduling.Numero,
                            Rua = scheduling.Rua
                        }
                    };
                }

                return scheduling;
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

        public IList<Scheduling> Get()
        {
            try
            {
                _connection.Open();
                var query = "SELECT Scheduling.SchedulingKey, " +
                    "DATE_FORMAT(Scheduling.Data, '%d/%m/%Y %H:%i:%s') as Data, " +
                    "DATE_FORMAT(Scheduling.Hora, '%d/%m/%Y %H:%i:%s') as Hora, " +
                    "Scheduling.Tipo, Scheduling.Status, Address.* " +
                    "FROM Scheduling Scheduling " +
                    "INNER JOIN Address Address on Address.AddressKey = Scheduling.Addresskey " +
                    "WHERE Scheduling.CitezenKey = @CitezenKey";
                var citezenKey = this.Context.Citezen.CitezenKey;

                var schedulings = _connection.Query<dynamic>(query, new { CitezenKey = citezenKey }).AsList();
                IList<Scheduling> response = new List<Scheduling>();

                schedulings.ForEach(scheduling =>
                {
                    response.Add(new Scheduling()
                    {
                        SchedulingKey = scheduling.SchedulingKey,
                        Data = DateTime.Parse(scheduling.Data),
                        Hora = DateTime.Parse(scheduling.Hora),
                        Tipo = scheduling.Tipo,
                        Status = scheduling.Status,
                        Address = new Address()
                        {
                            AddressKey = scheduling.AddressKey,
                            Bairro = scheduling.Bairro,
                            Cep = scheduling.Cep,
                            Cidade = scheduling.Cidade,
                            Estado = scheduling.Estado,
                            Numero = scheduling.Numero,
                            Rua = scheduling.Rua
                        }
                    });
                });
                return response;
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

        public override Scheduling Insert(Scheduling entity)
        {
            try
            {
                _connection.Open();
                entity.Citezen = this.Context.Citezen;
                _connection.Execute("INSERT INTO Scheduling (SchedulingKey, Data, Hora, Tipo, Status, AddressKey, CitezenKey) VALUES (@SchedulingKey, @Data, @Hora, @Tipo, @Status, @AddressKey, @CitezenKey)", new { SchedulingKey = entity.SchedulingKey, Data = entity.Data.Date, Hora = entity.Hora.TimeOfDay, Tipo = entity.Tipo, Status = entity.Status, AddressKey = entity.Address.AddressKey, CitezenKey = entity.Citezen.CitezenKey });
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

        public override Scheduling Update(Scheduling entity)
        {
            try
            {
                _connection.Open();
                _connection.Execute("UPDATE Scheduling SET Data = @Data, Hora = @Hora, Tipo = @Tipo, Status = @Status, AddressKey = @AddressKey where SchedulingKey = @SchedulingKey", entity);
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
    }
}
