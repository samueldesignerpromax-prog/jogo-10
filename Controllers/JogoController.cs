using Microsoft.AspNetCore.Mvc;
using JogoBackend.Services;
using JogoBackend.Models;

namespace JogoBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JogoController : ControllerBase
{
    private readonly JogoService _jogoService;

    public JogoController(JogoService jogoService)
    {
        _jogoService = jogoService;
    }

    // Criar novo jogador
    [HttpPost("criar-jogador")]
    public IActionResult CriarJogador([FromBody] string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return BadRequest("Nome é obrigatório");
        
        var jogador = _jogoService.CriarJogador(nome);
        return Ok(new { 
            message = $"Jogador {nome} criado com sucesso!",
            jogador = jogador 
        });
    }

    // Listar ranking de jogadores
    [HttpGet("ranking")]
    public IActionResult ObterRanking()
    {
        var ranking = _jogoService.ListarJogadores();
        return Ok(ranking);
    }

    // Iniciar nova partida
    [HttpPost("iniciar-partida/{jogadorId}")]
    public IActionResult IniciarPartida(string jogadorId)
    {
        var partida = _jogoService.IniciarPartida(jogadorId);
        if (partida == null)
            return NotFound("Jogador não encontrado");
        
        return Ok(new { 
            message = "Partida iniciada!",
            partida = partida 
        });
    }

    // Fazer jogada
    [HttpPost("jogar/{partidaId}")]
    public async Task<IActionResult> FazerJogada(string partidaId, [FromBody] int palpite)
    {
        if (palpite < 1 || palpite > 100)
            return BadRequest("Palpite deve estar entre 1 e 100");
        
        var jogada = await _jogoService.FazerJogada(partidaId, palpite);
        if (jogada == null)
            return NotFound("Partida não encontrada ou já finalizada");
        
        var partida = _jogoService.ObterPartida(partidaId);
        
        return Ok(new { 
            jogada = jogada,
            partida = partida,
            message = jogada.Resultado
        });
    }

    // Status da partida
    [HttpGet("status/{partidaId}")]
    public IActionResult StatusPartida(string partidaId)
    {
        var partida = _jogoService.ObterPartida(partidaId);
        if (partida == null)
            return NotFound("Partida não encontrada");
        
        return Ok(partida);
    }

    // Histórico de jogadas
    [HttpGet("historico/{partidaId}")]
    public IActionResult HistoricoJogadas(string partidaId)
    {
        var historico = _jogoService.ObterHistorico(partidaId);
        return Ok(historico);
    }

    // Ranking das melhores partidas
    [HttpGet("ranking-partidas")]
    public IActionResult RankingPartidas()
    {
        var ranking = _jogoService.ObterRankingPartidas();
        return Ok(ranking);
    }
}
