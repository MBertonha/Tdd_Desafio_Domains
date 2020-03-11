using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Matricula
{
    public class MatriculaDto
    {
        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
        public int AlunoId { get; set; }
        public int CursoId { get; set; }
    }
}
