using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Util;
using System;
using Xunit;

namespace CursoOnline.DominioTest.PublicosAlvo
{
    public class ConversorDePublicoAlvoTest
    {
        private readonly ConversorDePublicoAlvo _conversor = new ConversorDePublicoAlvo();

        [Theory]
        [InlineData(PublicoAlvo.Empregado, "Empregado")]
        [InlineData(PublicoAlvo.Empreendedor, "Empreendedor")]
        [InlineData(PublicoAlvo.Estudante, "Estudante")]
        [InlineData(PublicoAlvo.Universitário, "Universitário")]
        public void DeveConverterPublicoAlvo(PublicoAlvo publicoAlvoEsperado, string publicoAlvoEmString)
        {
            var publicoAlvoConvertido = _conversor.Converter(publicoAlvoEmString);

            Assert.Equal(publicoAlvoEsperado, publicoAlvoConvertido);
        }

        [Fact]
        public void NaoConverterPublicoAlvoInvalido()
        {
            const string publicoAlvoInvalido = "Invalido";  
            var conversor = new ConversorDePublicoAlvo();

            Assert.Throws<ExcecaoDeDominio>(() =>
                conversor.Converter(publicoAlvoInvalido))
                .ComMensagem(Resource.PublicoAlvoInvalido);
        }
    }

    public class ConversorDePublicoAlvo
    {
        public PublicoAlvo Converter(string publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(!Enum.TryParse<PublicoAlvo>(publicoAlvo, out var publicoAlvoConvertido), Resource.PublicoAlvoInvalido)
                .DispararExcecaoSeExistir();
                
            return publicoAlvoConvertido;
        }
    }
}