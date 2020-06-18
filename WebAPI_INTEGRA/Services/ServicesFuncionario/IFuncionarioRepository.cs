﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_INTEGRA.Models;

namespace WebAPI_INTEGRA.Services.ServicesFuncionario
{
    public interface IFuncionarioRepository
    {
        IEnumerable<Funcionario> GetAll();
        Funcionario GetById(int id);
        void AddFuncionario(Funcionario pessoas);
        void UpdateFuncionario(Funcionario pessoas);
        void DeleteFuncionario(int id);
    }
}
