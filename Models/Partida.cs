namespace JogoBackend.Models;

public class Partida
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string JogadorId { get; set; } = string.Empty;
    public string JogadorNome { get; set; } = string.Empty;
    public int PontuacaoFinal { get; set; }
    public int Tentativas { get; set; }
    public DateTime DataInicio { get; set; } = DateTime.UtcNow;
    public DateTime? DataFim { get; set; }
    public bool Finalizada { get; set; }
}
