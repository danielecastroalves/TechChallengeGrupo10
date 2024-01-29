namespace FintechGrupo10.Domain.Entities
{
    public class QuestionEntity : Entity
    {
        public string Titulo { get; set; } = null!;
        public List<Answer> Resposta { get; set; } = null!;
    }

    public class Answer
    {
        public string Descricao { get; set; } = null!;
        public int Pontuacao { get; set; }
    }
}
