using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
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
        private Aluno() { }

        public Aluno(string nome, string email, string cpf, PublicoAlvo publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .Quando(this.ValidaEmail(email), Resource.EmailInvalido)
                .Quando(!this.ValidaCPF(cpf), Resource.CpfInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
            Email = email;
            Cpf = cpf;
            PublicoAlvo = publicoAlvo;
        }
    }
}
