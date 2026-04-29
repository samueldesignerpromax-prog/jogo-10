using Microsoft.AspNetCore.Mvc;

namespace JogoBackend.Controllers;

public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        var html = @"
<!DOCTYPE html>
<html>
<head>
    <title>🎮 Jogo Backend API</title>
    <meta charset='UTF-8'>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
        }
        .container {
            background: rgba(255,255,255,0.1);
            border-radius: 20px;
            padding: 30px;
            backdrop-filter: blur(10px);
        }
        h1 { text-align: center; }
        .endpoint {
            background: rgba(0,0,0,0.3);
            padding: 10px;
            margin: 10px 0;
            border-radius: 5px;
            font-family: monospace;
        }
        .method {
            display: inline-block;
            padding: 3px 8px;
            border-radius: 3px;
            font-weight: bold;
            margin-right: 10px;
        }
        .get { background: #61affe; }
        .post { background: #49cc90; }
        a { color: white; text-decoration: none; }
        .status { text-align: center; margin-top: 20px; font-size: 14px; }
    </style>
</head>
<body>
    <div class='container'>
        <h1>🎮 Jogo Backend API</h1>
        <p>API do jogo de adivinhação de números - Deploy no Render 🚀</p>
        
        <h2>📡 Endpoints Disponíveis:</h2>
        
        <div class='endpoint'>
            <span class='method post'>POST</span>
            <code>/api/Jogo/criar-jogador</code>
            <span> - Criar novo jogador</span>
        </div>
        
        <div class='endpoint'>
            <span class='method get'>GET</span>
            <code>/api/Jogo/ranking</code>
            <span> - Ver ranking dos jogadores</span>
        </div>
        
        <div class='endpoint'>
            <span class='method post'>POST</span>
            <code>/api/Jogo/iniciar-partida/{jogadorId}</code>
            <span> - Iniciar nova partida</span>
        </div>
        
        <div class='endpoint'>
            <span class='method post'>POST</span>
            <code>/api/Jogo/jogar/{partidaId}</code>
            <span> - Fazer uma jogada</span>
        </div>
        
        <div class='endpoint'>
            <span class='method get'>GET</span>
            <code>/api/Jogo/status/{partidaId}</code>
            <span> - Ver status da partida</span>
        </div>
        
        <div class='endpoint'>
            <span class='method get'>GET</span>
            <code>/api/Jogo/historico/{partidaId}</code>
            <span> - Ver histórico de jogadas</span>
        </div>
        
        <div class='endpoint'>
            <span class='method get'>GET</span>
            <code>/swagger</code>
            <span> - Documentação interativa (Swagger UI)</span>
        </div>
        
        <div class='status'>
            ✅ API Online - Último deploy: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + @"
        </div>
    </div>
</body>
</html>";
        
        return Content(html, "text/html");
    }
    
    [HttpGet("/health")]
    public IActionResult Health()
    {
        return Ok(new { 
            status = "online", 
            timestamp = DateTime.UtcNow,
            versao = "1.0.0",
            endpoints = new[] {
                "/swagger",
                "/api/Jogo/criar-jogador",
                "/api/Jogo/ranking",
                "/api/Jogo/iniciar-partida/{jogadorId}",
                "/api/Jogo/jogar/{partidaId}"
            }
        });
    }
}
