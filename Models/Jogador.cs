namespace JogoBackend.Models;

public class Jogador
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Nome { get; set; } = string.Empty;
    public int PontuacaoTotal { get; set; }
    public int PartidasJogadas { get; set; }
    public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
}
