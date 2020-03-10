using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CursoOnline.DominioTest.Alunos
{
    public class ArmazenadorDeAlunoTest
    {
        private readonly AlunoDto _alunoDto;
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;

        public ArmazenadorDeAlunoTest()
        {
            var _fake = new Faker();
            var _dta = _fake.Date.Past();
            _alunoDto = new AlunoDto
            {
                Nome = _fake.Person.FullName,
                Email = _fake.Person.Email,
                Cpf = _fake.Person.Cpf(),
                PublicoAlvo = PublicoAlvo.Empreendedor.ToString(),
                DataNasc = _dta.ToString("dd/MM/yyyy")
            };

            _alunoRepositorioMock = new Mock<IAlunoRepositorio>();
            _armazenadorDeAluno = new ArmazenadorDeAluno(_alunoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarAluno()
        {
            _armazenadorDeAluno.Armazenar(_alunoDto);

            _alunoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Aluno>(
                    c => c.Nome == _alunoDto.Nome
                )));
        }

        [Fact]
        public void NaoDeveAdicionarAlunoComMesmoNomeDeOutroJaSalvo()
        {
            var alunoJaSalvo = AlunoBuilder.Novo().ComId(432).ComNome(_alunoDto.Nome).Build();
            _alunoRepositorioMock.Setup(r => r.ObterPeloNome(_alunoDto.Nome)).Returns(alunoJaSalvo);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeAluno.Armazenar(_alunoDto))
                .ComMensagem(Resource.NomeDoAlunoJaExiste);
        }

        [Fact]
        public void NaoDeveAdicionarAlunoComMesmoCpfDeOutroJaSalvo()
        {
            var cpfJaSalvo = AlunoBuilder.Novo().ComId(432).ComCpf(_alunoDto.Cpf).Build();
            _alunoRepositorioMock.Setup(r => r.ObterPeloCpf(_alunoDto.Cpf)).Returns(cpfJaSalvo);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeAluno.Armazenar(_alunoDto))
                .ComMensagem(Resource.CpfDoAlunoJaExiste);
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoAlunoJaExiste()
        {
            _alunoDto.Id = 323;
            var aluno = AlunoBuilder.Novo().Build();
            _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(aluno);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            _alunoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never);
        }
    }
}
