using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Alunos
{
    public class ArmazenadorDeAluno
    {
        private readonly IAlunoRepositorio _alunoRepositorio;

        public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public void Armazenar(AlunoDto alunoDto)
        {
            var alunoComMesmoCpf = _alunoRepositorio.ObterPeloCpf(alunoDto.Cpf);

            ValidadorDeRegra.Novo()
                .Quando(alunoComMesmoCpf != null && alunoComMesmoCpf.Id != alunoDto.Id, Resource.CpfDoAlunoJaExiste)
                .Quando(!Enum.TryParse<PublicoAlvo>(alunoDto.PublicoAlvo, out var publicoAlvo), Resource.PublicoAlvoInvalido)
                .DispararExcecaoSeExistir();

            if (alunoDto.Id == 0)
            {
                var aluno = new Aluno(alunoDto.Nome, alunoDto.Email, alunoDto.Cpf, alunoDto.DataNasc, publicoAlvo);
                _alunoRepositorio.Adicionar(aluno);
            }
            else
            {
                var aluno = _alunoRepositorio.ObterPeloNome(alunoDto.Nome);
                aluno.AlterarNome(alunoDto.Nome);
            }
        }
    }
}
