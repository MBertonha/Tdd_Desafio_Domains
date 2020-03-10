using System;
using System.Collections.Generic;
using System.Text;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matricula;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTest.Cursos;
using static CursoOnline.DominioTest.Matriculas.MatriculaTest;

namespace CursoOnline.DominioTest._Builders
{
    public class MatriculaBuilder
    {
        protected Aluno Aluno;
        protected Curso Curso;
        protected double ValorPago;

        public static MatriculaBuilder Novo()
        {
            var curso = CursoBuilder.Novo().Build();

            return new MatriculaBuilder
            {
                Aluno = AlunoBuilder.Novo().Build(),
                Curso = curso,
                ValorPago = curso.Valor 
            };
        }

        public MatriculaBuilder ComAluno(Aluno aluno)
        {
            Aluno = aluno;
            return this;
        }

        public MatriculaBuilder ComCurso(Curso curso)
        {
            Curso = curso;
            return this;
        }

        public MatriculaBuilder ComValorPago(double valorPago)
        {
            ValorPago = valorPago;
            return this;
        }   

        public Matricula Build()
        {
            return new Matricula(Aluno, Curso, ValorPago);
        }

        internal object ComAluno(Curso cursoInvalido)
        {
            throw new NotImplementedException();
        }
    }
}
