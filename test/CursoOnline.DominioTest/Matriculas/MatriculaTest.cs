using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matricula;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class MatriculaTest
    {
        [Fact]
        public void DeveCriarMatricula()
        {
            var faker = new Faker();
            var curso = CursoBuilder.Novo().Build();

            var matriculaEsperada = new
            {
                Aluno = AlunoBuilder.Novo().Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };

            var matricula = new Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);

            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemAluno()
        {
            Aluno alunoInvalido = null;

            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComAluno(alunoInvalido).Build())
                .ComMensagem(Resource.AlunoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemCurso()
        {
            Curso cursoInvalido = null;

            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComCurso(cursoInvalido).Build())
                .ComMensagem(Resource.CursoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorInvalido()
        {
            const double valorPagoInvalido = 0;

            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComValorPago(valorPagoInvalido).Build())
                .ComMensagem(Resource.ValorInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoMaiorQueValorDoCurso()
        {
            var curso = CursoBuilder.Novo().ComValor(100).Build();
            var valorPagoMaiorQueCurso = curso.Valor + 1; 

            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPagoMaiorQueCurso).Build())
                .ComMensagem(Resource.ValorInvalido);
        }
    }
}
