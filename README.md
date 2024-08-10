# criptomoeda.api

**criptomoeda.api** é um projeto que consome a api do mercadobitcoin (https://www.mercadobitcoin.com.br/api-doc/) para retornar informações com o resumo das últimas 24 horas de negociações.

O projeto é um modelo de arquitetura de software para projetos .NET Core 6.0 baseado na Arquitetura Hexagonal (Alistair Cockburn).

## Scripts

Obter negociacao mais atual de uma cripto:
var entidade = await _context.NegociacoesDoDia
    .Where(x => x.Sigla == sigla)
    .FirstOrDefaultAsync();

Atualizar negoaciacao:
_context.Entry(negociacoesExistente)
        .CurrentValues.SetValues(model);

Obter registro de negociacoes de uma cripto:
var registroNegociacoes = await _context.HistoricoNegociacoes
                            .Where(x => x.Sigla == sigla)
                            .OrderByDescending(x => x.DataHora)
                            .ToListAsync();
