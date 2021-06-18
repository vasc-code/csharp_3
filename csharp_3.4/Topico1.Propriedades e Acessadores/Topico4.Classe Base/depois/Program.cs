﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topico4
{
    class Program
    {
        static void Main(string[] args)
        {
            Funcionario funcionario = new Funcionario(1500);
            funcionario.CPF = "123.456.789-00";
            funcionario.Nome = "josé da silva";
            funcionario.DataNascimento = new DateTime(2000, 1, 1);

            ((IFuncionario)funcionario).CargaHorariaMensal = 168;
            ((IPlantonista)funcionario).CargaHorariaMensal = 32;
            funcionario.EfeturarPagamento();
            funcionario.CrachaGerado += (s, e) =>
            {
                Console.WriteLine("Crachá gerado");
            };
            ((IFuncionario)funcionario).GerarCracha();
            ((IPlantonista)funcionario).GerarCracha();

            Cliente cliente = new Cliente
            {
                CPF = "789.456.123-20",
                DataNascimento = new DateTime(2000, 6, 18),
                Nome = "Rafael Martins",
                DataUltimaCompra = new DateTime(2021,6,8),
                ValorUltimaCompra = 200
            };

            Console.WriteLine(cliente);

            Pessoa pessoa = new Cliente()
            {
                CPF = "789.456.123-20",
                DataNascimento = new DateTime(2000, 6, 18),
                Nome = "Rafael Martins",
            };
        }
    }

    interface IFuncionario
    {
        string CPF { get; set; }
        string Nome { get; set; }
        DateTime DataNascimento { get; set; }

        event EventHandler CrachaGerado;

        void GerarCracha();

        decimal Salario { get; }
        int CargaHorariaMensal { get; set; }

        void EfeturarPagamento();
    }

    interface IPlantonista
    {
        int CargaHorariaMensal { get; set; }
        void GerarCracha();
    }

    class Funcionario : Pessoa, IFuncionario, IPlantonista
    {
       
        public event EventHandler CrachaGerado;

        void IFuncionario.GerarCracha()
        {
            if (CrachaGerado != null)
            {
                CrachaGerado(this, new EventArgs());
            }
        }

        void IPlantonista.GerarCracha()
        {
            if (CrachaGerado != null)
            {
                CrachaGerado(this, new EventArgs());
            }
        }

        public decimal Salario { get; }

        int IFuncionario.CargaHorariaMensal { get; set; }

        int IPlantonista.CargaHorariaMensal { get; set; }

        public Funcionario(decimal salario)
        {
            Salario = salario;
        }

        public void EfeturarPagamento()
        {
            Console.WriteLine("Pagamento Efetuado");
        }
    }

    sealed class Cliente : Pessoa
    {
     
        public DateTime? DataUltimaCompra { get; set; }
        public decimal? ValorUltimaCompra { get; set; }

        public override string ToString()
        {
            return $"Nome: {Nome}, DataUltimaCompra: {DataUltimaCompra}";
        }
    }

    abstract class Pessoa
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
