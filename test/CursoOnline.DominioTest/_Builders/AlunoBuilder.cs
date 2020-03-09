using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.DominioTest._Builders
{
    public class AlunoBuilder
    {
        private int _id;
        private string _nome = "";
        private string _email = "";
        private string _cpf = "";
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;

        public static AlunoBuilder Novo()
        {
            return new AlunoBuilder();
        }

        public AlunoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public AlunoBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public AlunoBuilder ComCpf(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public AlunoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public AlunoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public Aluno Build()
        {
            var aluno = new Aluno(_nome, _email, _cpf, _publicoAlvo);

            if (_id > 0)
            {
                var propertyInfo = aluno.GetType().GetProperty("Id");
                propertyInfo.SetValue(aluno, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return aluno;
        }
    }
}
