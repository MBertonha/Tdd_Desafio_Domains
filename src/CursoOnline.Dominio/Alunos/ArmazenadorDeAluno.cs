using CursoOnline.Dominio._Base;
using System;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.Dominio.Alunos
{
    public class ArmazenadorDeAluno
    {
        private readonly IConversorDePublicoAlvo _conversorDePublicoAlvo;
        private readonly IAlunoRepositorio _alunoRepositorio;

        public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio, IConversorDePublicoAlvo conversorDePublicoAlvo)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public void Armazenar(AlunoDto alunoDto)
        {
            var alunoComMesmoCpf = _alunoRepositorio.ObterPeloCpf(alunoDto.Cpf);

            ValidadorDeRegra.Novo()
                .Quando(alunoComMesmoCpf != null && alunoComMesmoCpf.Id != alunoDto.Id, Resource.CpfDoAlunoJaExiste)
                .DispararExcecaoSeExistir();
        

            if (alunoDto.Id == 0)
            {
                var publicoAlvoConvertido = _conversorDePublicoAlvo.Converter(alunoDto.PublicoAlvo);
                var aluno = new Aluno(alunoDto.Nome, alunoDto.Email, alunoDto.Cpf, alunoDto.DataNasc, publicoAlvoConvertido);
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
