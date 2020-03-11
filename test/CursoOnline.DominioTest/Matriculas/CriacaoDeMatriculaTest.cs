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
        private readonly Mock<ICursoRepositorio> _cursoRepositorio;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private readonly Aluno _aluno;
        private readonly MatriculaDto _matriculaDto;
        private readonly CriacaoDeMatricula _criracaoDeMatricula;
        private readonly Curso _curso;

        public CriacaoDeMatriculaTest(){

            _cursoRepositorio = new Mock<ICursoRepositorio>();
            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();

            _aluno = AlunoBuilder.Novo().ComId(33).ComPublicoAlvo(PublicoAlvo.Universitário).Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_aluno.Id)).Returns(_aluno);

            _curso = CursoBuilder.Novo().ComId(5).ComPublicoAlvo(PublicoAlvo.Universitário).Build();
            _cursoRepositorio.Setup(r => r.ObterPorId(_curso.Id)).Returns(_curso);

            _matriculaDto = new MatriculaDto { AlunoId = _aluno.Id, CursoId = _curso.Id, ValorPago = _curso.Valor };

            _criracaoDeMatricula = new CriacaoDeMatricula(_alunoRepositorio.Object, _cursoRepositorio.Object, _matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveHaverCursoCadastrado()
        {
            Curso cursoInvalido = null;
            _cursoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.CursoId)).Returns(cursoInvalido);

            Assert.Throws<ExcecaoDeDominio>(() =>
                _criracaoDeMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.CursoNaoEncontrado);
        }

        [Fact]
        public void DeveHaverAlunoCadastrado()
        {
            Aluno alunoInvalido = null;
            _alunoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.AlunoId)).Returns(alunoInvalido);

            Assert.Throws<ExcecaoDeDominio>(() =>
                _criracaoDeMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.AlunoNaoEncontrado);
        }

        [Fact]
        public void DeveAdicionarMatricula()
        {
            _criracaoDeMatricula.Criar(_matriculaDto);
            _matriculaRepositorio.Verify(r => r.Adicionar(
                It.Is<Matricula>(m => m.Aluno == _aluno && m.Curso == _curso))
            );
        }
    }

}
