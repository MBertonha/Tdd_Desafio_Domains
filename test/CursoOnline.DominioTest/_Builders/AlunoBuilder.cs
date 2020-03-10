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
        private string _nome = "Matheus Bertonha";
        private string _email = "matheus@hotmail.com";
        private string _cpf = "26138991079";
        private string _dataNasc = "14/05/2000";
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
        public AlunoBuilder ComData(string dataNasc)
        {
            _dataNasc = dataNasc;
            return this;
        }

        public AlunoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public Dominio.Alunos.Aluno Build()
        {
            var aluno = new Dominio.Alunos.Aluno(_nome, _email, _cpf, _dataNasc, _publicoAlvo);

            if (_id > 0)
            {
                var propertyInfo = aluno.GetType().GetProperty("Id");
                propertyInfo.SetValue(aluno, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return aluno;
        }
    }
}
