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
    public class AgendamentoRepository : RepositoryBase<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(IHttpContextAccessor context, IDbConnection connection) : base(connection, context) { }

        public override bool Delete(Guid key)
        {
            try
            {
                _connection.Open();
                return _connection.Execute("DELETE FROM Agendamento WHERE AgendamentoKey = @AgendamentoKey", new { AgendamentoKey = key }) > 0;
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

        public bool Exists(Agendamento agendamento)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<bool>("SELECT 1 FROM Agendamento WHERE Data = @Data AND Hora = @Hora LIMIT 1", agendamento);
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

        public override Agendamento Get(Guid key)
        {
            try
            {
                _connection.Open();
                var agendamento =  _connection.QueryFirstOrDefault<dynamic>("SELECT Agendamento.AgendamentoKey, " +
                    "DATE_FORMAT(Agendamento.Data, '%d/%m/%Y %H:%i:%s') as Data, " +
                    "DATE_FORMAT(Agendamento.Hora, '%d/%m/%Y %H:%i:%s') as Hora, " +
                    "Agendamento.Tipo, Agendamento.Status, Endereco.* " +
                    "FROM Agendamento " +
                    "INNER JOIN Endereco on Endereco.enderecoKey = Agendamento.Enderecokey " +
                    "WHERE AgendamentoKey = @AgendamentoKey LIMIT 1", new { AgendamentoKey = key });

                return new Agendamento()
                {
                    AgendamentoKey = agendamento.AgendamentoKey,
                    Data = DateTime.Parse(agendamento.Data),
                    Hora = DateTime.Parse(agendamento.Hora),
                    Tipo = agendamento.Tipo,
                    Status = agendamento.Status,
                    Endereco = new Local()
                    {
                        EnderecoKey = agendamento.EnderecoKey,
                        Bairro = agendamento.Bairro,
                        Cep = agendamento.Cep,
                        Cidade = agendamento.Cidade,
                        Estado = agendamento.Estado,
                        Numero = agendamento.Numero,
                        Rua = agendamento.Rua
                    }
                };
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

        public IList<Agendamento> Get()
        {
            try
            {
                _connection.Open();
                var query = "SELECT Agendamento.AgendamentoKey, " +
                    "DATE_FORMAT(Agendamento.Data, '%d/%m/%Y %H:%i:%s') as Data, " +
                    "DATE_FORMAT(Agendamento.Hora, '%d/%m/%Y %H:%i:%s') as Hora, " +
                    "Agendamento.Tipo, Agendamento.Status, Endereco.* " +
                    "FROM Agendamento Agendamento " +
                    "INNER JOIN Endereco Endereco on Endereco.EnderecoKey = Agendamento.Enderecokey " +
                    "WHERE Agendamento.CidadaoKey = @CidadaoKey";
                var cidadaoKey = this.Context.Cidadao.CidadaoKey;

                var agendamentos = _connection.Query<dynamic>(query, new { CidadaoKey = cidadaoKey }).AsList();
                IList<Agendamento> response = new List<Agendamento>();

                agendamentos.ForEach(agendamento =>
                {
                    response.Add(new Agendamento()
                    {
                        AgendamentoKey = agendamento.AgendamentoKey,
                        Data = DateTime.Parse(agendamento.Data),
                        Hora = DateTime.Parse(agendamento.Hora),
                        Tipo = agendamento.Tipo,
                        Status = agendamento.Status,
                        Endereco = new Local()
                        {
                            EnderecoKey = agendamento.EnderecoKey,
                            Bairro = agendamento.Bairro,
                            Cep = agendamento.Cep,
                            Cidade = agendamento.Cidade,
                            Estado = agendamento.Estado,
                            Numero = agendamento.Numero,
                            Rua = agendamento.Rua
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

        public override Agendamento Insert(Agendamento entity)
        {
            try
            {
                _connection.Open();
                entity.Cidadao = this.Context.Cidadao;
                _connection.Execute("INSERT INTO Agendamento (AgendamentoKey, Data, Hora, Tipo, Status, EnderecoKey, CidadaoKey) VALUES (@AgendamentoKey, @Data, @Hora, @Tipo, @Status, @EnderecoKey, @CidadaoKey)", new { AgendamentoKey = entity.AgendamentoKey, Data = entity.Data.Date, Hora = entity.Hora.TimeOfDay, Tipo = entity.Tipo, Status = entity.Status, EnderecoKey = entity.Endereco.EnderecoKey, CidadaoKey = entity.Cidadao.CidadaoKey });
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

        public override Agendamento Update(Agendamento entity)
        {
            try
            {
                _connection.Open();
                _connection.Execute("UPDATE Agendamento SET Data = @Data, Hora = @Hora, Tipo = @Tipo, Status = @Status, EnderecoKey = @EnderecoKey where AgendamentoKey = @AgendamentoKey", entity);
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
