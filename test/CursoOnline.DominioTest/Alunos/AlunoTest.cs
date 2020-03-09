using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Cursos;
using ExpectedObjects;
using CursoOnline.Dominio.Alunos;
using Xunit;
using CursoOnline.DominioTest._Builders;

namespace CursoOnline.DominioTest.Alunos
{
    public class AlunoTest
    {

        private readonly string _nome;
        private readonly string _email;
        private readonly string _cpf;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly Faker _faker;

        public AlunoTest()
        {
            _faker = new Faker();

            _nome = _faker.Random.Word();
            _email = _faker.Person.Email;
            _cpf = _faker.Person.Cpf();
            _publicoAlvo = PublicoAlvo.Universitário;
        }

        [Fact]
        public void CriarAluno()
        {
            var alunoEsperado = new
            {
                Nome = _nome,
                Email = _email,
                Cpf = _cpf,
                PublicoAlvo = _publicoAlvo
            };

            var aluno = new Aluno(alunoEsperado.Nome, alunoEsperado.Email, alunoEsperado.Cpf, alunoEsperado.PublicoAlvo);

            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var novoNomeEsperado = _faker.Person.FullName;
            var aluno = AlunoBuilder.Novo().Build();

            aluno.AlterarNome(novoNomeEsperado);

            Assert.Equal(novoNomeEsperado, aluno.Nome);
        }

    }
}
