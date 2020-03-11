using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matricula;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class CriacaoDeMatriculaTest
    {
        private Mock<ICursoRepositorio> _cursoRepositorio;
        private Mock<IAlunoRepositorio> _alunoRepositorio;
        private MatriculaDto _matriculaDto;
        private CriacaoDeMatricula _ciracaoDeMatricula;

        public CriacaoDeMatriculaTest(){

            _cursoRepositorio = new Mock<ICursoRepositorio>();
            _alunoRepositorio = new Mock<IAlunoRepositorio>();

            var aluno = AlunoBuilder.Novo().ComId(33).ComPublicoAlvo(PublicoAlvo.Universitário).Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(aluno.Id)).Returns(aluno);

            var curso = CursoBuilder.Novo().ComId(5).ComPublicoAlvo(PublicoAlvo.Universitário).Build();
            _cursoRepositorio.Setup(r => r.ObterPorId(curso.Id)).Returns(curso);

            _matriculaDto = new MatriculaDto { AlunoId = aluno.Id, CursoId = curso.Id };

            _ciracaoDeMatricula = new CriacaoDeMatricula(_alunoRepositorio.Object, _cursoRepositorio.Object);
        }

        [Fact]
        public void DeveHaverCursoCadastrado()
        {
            Curso cursoInvalido = null;
            _cursoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.CursoId)).Returns(cursoInvalido);

            Assert.Throws<ExcecaoDeDominio>(() =>
                _ciracaoDeMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.CursoNaoEncontrado);
        }

        [Fact]
        public void DeveHaverAlunoCadastrado()
        {
            Aluno alunoInvalido = null;
            _alunoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.AlunoId)).Returns(alunoInvalido);

            Assert.Throws<ExcecaoDeDominio>(() =>
                _ciracaoDeMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.AlunoNaoEncontrado);
        }
    }

}
