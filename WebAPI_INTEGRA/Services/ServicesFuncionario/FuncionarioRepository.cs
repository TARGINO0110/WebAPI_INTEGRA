﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_INTEGRA.Models;

namespace WebAPI_INTEGRA.Services.ServicesFuncionario
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        public readonly IConfiguration _configuration;

        // ******** ACESSO AO BANCO SOBRE O ARQUIVO JSON 'appsettings.json', CONCEDIDO PELA INTERFACE 'IConfiguration' DA CLASSE 'Startup' ********
        public FuncionarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // ******** ACESSO AO BANCO COM 'ConnectionString' NA CLASSE LOCAL ********

        //public string ConnectionString;
        //public FuncionarioRepository()
        //{
        //    ConnectionString = @"Data Source=Seu HostName;Initial Catalog=Seu Banco;Integrated Security=True";
        //}

        //public IDbConnection Connection
        //{
        //    //get { return new SqlConnection(ConnectionString); }
        //}

        public IDbConnection Connection
        {
            get { return new SqlConnection(_configuration.GetConnectionString("DefaultConnection")); }
            //get { return new OracleConnection(_configuration.GetConnectionString("DefaultConnection")); } UTILIZAR EM BANCO ORACLE
        }
        //OBS : PARA USO DOS BIND EM ORACLE TROCAR O [@ POR :] POR EXEMPO => sQuery = @ INSERT INTO TABELA (COLUNA1,COLUNA2) VALUES (:COLUNA1,:COLUNA2);

        public async Task<IEnumerable<Profissional>> GetAll()
        {
            using IDbConnection dbConnection = Connection;
            try
            {
                string sQuery = @"SELECT * FROM FUNCIONARIO";
                dbConnection.Open();
                var listaSql = await dbConnection.QueryAsync<Profissional>(sQuery);
                return (listaSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public async Task<Profissional> GetById(int id)
        {
            using IDbConnection dbConnection = Connection;
            try
            {
                string sQuery = @"SELECT * FROM FUNCIONARIO WHERE FuncionarioId = @Id";
                dbConnection.Open();
                var listaIdSql = await dbConnection.QueryAsync<Profissional>(sQuery, new { Id = id });
                return (listaIdSql).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void AddFuncionario(Profissional funcionario)
        {
            using IDbConnection dbConnection = Connection;
            try
            {
                string sQuery = @"INSERT INTO FUNCIONARIO (NomeFuncionario,FuncaoFunc,CPF,Salario) 
                                VALUES (@NomeFuncionario,@FuncaoFunc,@CPF,@Salario)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, funcionario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void UpdateFuncionario(Profissional funcionario)
        {
            using IDbConnection dbConnection = Connection;
            try
            {
                string sQuery = @"UPDATE FUNCIONARIO SET NomeFuncionario=@NomeFuncionario,
                                                         FuncaoFunc=@FuncaoFunc,
                                                         CPF=@CPF,
                                                         Salario=@Salario
                                                         WHERE FuncionarioId = @FuncionarioId";

                dbConnection.Open();
                dbConnection.Query(sQuery, funcionario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void DeleteFuncionario(int id)
        {
            using IDbConnection dbConnection = Connection;
            try
            {
                string sQuery = @"DELETE FROM FUNCIONARIO WHERE FuncionarioId = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }
        }

    }
}
