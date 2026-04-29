using JogoBackend.Models;
using System.Collections.Concurrent;

namespace JogoBackend.Services;

public class JogoService
{
    private readonly ConcurrentDictionary<string, Jogador> _jogadores = new();
    private readonly ConcurrentDictionary<string, Partida> _partidas = new();
    private readonly ConcurrentDictionary<string, List<Jogada>> _jogadas = new();
    private readonly Random _random = new();

    // Criar novo jogador
    public Jogador CriarJogador(string nome)
    {
        var jogador = new Jogador { Nome = nome };
        _jogadores[jogador.Id] = jogador;
        return jogador;
    }

    // Obter jogador
    public Jogador? ObterJogador(string id)
    {
        _jogadores.TryGetValue(id, out var jogador);
        return jogador;
    }

    // Listar todos jogadores (ranking)
    public List<Jogador> ListarJogadores()
    {
        return _jogadores.Values
            .OrderByDescending(j => j.PontuacaoTotal)
            .ToList();
    }

    // Iniciar nova partida
    public Partida? IniciarPartida(string jogadorId)
    {
        var jogador = ObterJogador(jogadorId);
        if (jogador == null) return null;

        var partida = new Partida
        {
            JogadorId = jogadorId,
            JogadorNome = jogador.Nome,
            DataInicio = DateTime.UtcNow
        };
        
        _partidas[partida.Id] = partida;
        _jogadas[partida.Id] = new List<Jogada>();
        
        return partida;
    }

    // Fazer jogada (Adivinhar número)
    public async Task<Jogada?> FazerJogada(string partidaId, int palpite)
    {
        if (!_partidas.TryGetValue(partidaId, out var partida))
            return null;
        
        if (partida.Finalizada)
            return null;
        
        // Sortear número entre 1 e 100
        await Task.Delay(50); // Simula processamento
        var numeroSorteado = _random.Next(1, 101);
        
        var jogada = new Jogada
        {
            PartidaId = partidaId,
            NumeroSorteado = numeroSorteado,
            Palpite = palpite,
            DataJogada = DateTime.UtcNow
        };
        
        // Calcular resultado
        var diferenca = Math.Abs(palpite - numeroSorteado);
        
        if (diferenca == 0)
        {
            jogada.Resultado = "🎉 ACERTOU! 🎉";
            jogada.PontosGanhos = 100;
            partida.PontuacaoFinal += jogada.PontosGanhos;
            partida.Finalizada = true;
            partida.DataFim = DateTime.UtcNow;
            
            // Atualizar pontuação do jogador
            if (_jogadores.TryGetValue(partida.JogadorId, out var jogador))
            {
                jogador.PontuacaoTotal += partida.PontuacaoFinal;
                jogador.PartidasJogadas++;
            }
        }
        else if (diferenca <= 5)
        {
            jogada.Resultado = $"😮 QUASE! Diferença de {diferenca}";
            jogada.PontosGanhos = 50 - (diferenca * 5);
            partida.PontuacaoFinal += jogada.PontosGanhos;
        }
        else if (diferenca <= 10)
        {
            jogada.Resultado = $"😐 Foi perto! Diferença de {diferenca}";
            jogada.PontosGanhos = 30 - (diferenca - 5);
            partida.PontuacaoFinal += jogada.PontosGanhos;
        }
        else
        {
            jogada.Resultado = $"😅 Errou! Diferença de {diferenca}";
            jogada.PontosGanhos = 10;
            partida.PontuacaoFinal += jogada.PontosGanhos;
        }
        
        partida.Tentativas++;
        
        // Adicionar jogada
        _jogadas[partidaId].Add(jogada);
        
        return jogada;
    }

    // Obter status da partida
    public Partida? ObterPartida(string partidaId)
    {
        _partidas.TryGetValue(partidaId, out var partida);
        return partida;
    }

    // Obter histórico de jogadas
    public List<Jogada> ObterHistorico(string partidaId)
    {
        if (_jogadas.TryGetValue(partidaId, out var jogadas))
            return jogadas.OrderByDescending(j => j.DataJogada).ToList();
        
        return new List<Jogada>();
    }

    // Ranking das melhores partidas
    public List<Partida> ObterRankingPartidas()
    {
        return _partidas.Values
            .Where(p => p.Finalizada)
            .OrderByDescending(p => p.PontuacaoFinal)
            .Take(10)
            .ToList();
    }
}
