using Microsoft.AspNetCore.Mvc;

namespace JogoBackend.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        return Ok(new {
            mensagem = "🎮 Jogo Backend está rodando!",
            endpoints = new {
                criar_jogador = "POST /api/Jogo/criar-jogador",
                ranking = "GET /api/Jogo/ranking",
                iniciar_partida = "POST /api/Jogo/iniciar-partida/{jogadorId}",
                jogar = "POST /api/Jogo/jogar/{partidaId}",
                status = "GET /api/Jogo/status/{partidaId}",
                historico = "GET /api/Jogo/historico/{partidaId}",
                docs = "/swagger"
            },
            exemplo = new {
                como_usar = "Acesse /swagger para ver a documentação interativa"
            }
        });
    }

    [HttpGet("/health")]
    public IActionResult Health()
    {
        return Ok(new { 
            status = "online", 
            timestamp = DateTime.UtcNow,
            versao = "1.0.0"
        });
    }
}
