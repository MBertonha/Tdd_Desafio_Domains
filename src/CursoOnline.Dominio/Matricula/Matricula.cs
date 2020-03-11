using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Matricula
{
    public class Matricula
    {
        public object matricula;

        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public double ValorPago { get; private set; }
        public bool TemDesconto { get; private set; }
        public bool Cancelada { get; private set; }

        public Matricula(Aluno aluno, Curso curso, double valorPago)
        {
            ValidadorDeRegra.Novo()
                .Quando(aluno == null, Resource.AlunoInvalido)
                .Quando(curso == null, Resource.CursoInvalido)
                .Quando(valorPago < 1, Resource.ValorInvalido)
                .Quando(curso != null && valorPago > curso.Valor, Resource.ValorInvalido)
                .Quando(aluno != null && curso != null && aluno.PublicoAlvo != curso.PublicoAlvo, Resource.PublicoAlvoDiferente)
                .DispararExcecaoSeExistir();

            Aluno = aluno;
            Curso = curso;
            ValorPago = valorPago;
            TemDesconto = valorPago < Curso.Valor;
        }

        public void Cancelar()
        {
            Cancelada = true;
        }
    }
}
