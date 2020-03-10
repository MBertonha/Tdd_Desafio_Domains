using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicosAlvo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Alunos
{
    public class Aluno : Entidade
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public string DataNasc { get; private set; }
        private Aluno() { }

        public Aluno(string nome, string email, string cpf, string dataNasc, PublicoAlvo publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .Quando(this.ValidaEmail(email), Resource.EmailInvalido)
                .Quando(!this.ValidaCPF(cpf), Resource.CpfInvalido)
                .Quando(this.ValidarData(dataNasc), Resource.DataInvalida)
                .DispararExcecaoSeExistir();

            Nome = nome;
            Email = email;
            Cpf = cpf;
            PublicoAlvo = publicoAlvo;
            DataNasc = dataNasc;
        }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.ValorInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
        }

        public void AlteraDataNasc(string newData)
        {
            ValidadorDeRegra.Novo()
                .Quando(this.ValidarData(newData), Resource.DataInvalida)
                .DispararExcecaoSeExistir();

            DataNasc = newData;
        }
    }
}
