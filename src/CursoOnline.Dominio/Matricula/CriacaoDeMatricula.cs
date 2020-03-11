using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Matricula
{
    public class CriacaoDeMatricula
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly ICursoRepositorio _cursoRepositorio;

        public CriacaoDeMatricula(IAlunoRepositorio alunoRepositorio, ICursoRepositorio cursoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
            _cursoRepositorio = cursoRepositorio;
        }

        public void Criar(MatriculaDto matriculaDto)
        {
            var curso = _cursoRepositorio.ObterPorId(matriculaDto.CursoId);
            var aluno = _alunoRepositorio.ObterPorId(matriculaDto.AlunoId);

            ValidadorDeRegra.Novo()
                .Quando(curso == null, Resource.CursoNaoEncontrado)
                .Quando(aluno == null, Resource.AlunoNaoEncontrado)
                .DispararExcecaoSeExistir();
        }
    }
}
