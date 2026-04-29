namespace JogoBackend.Models;

public class Jogada
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PartidaId { get; set; } = string.Empty;
    public int NumeroSorteado { get; set; }
    public int Palpite { get; set; }
    public string Resultado { get; set; } = string.Empty;
    public int PontosGanhos { get; set; }
    public DateTime DataJogada { get; set; } = DateTime.UtcNow;
}
