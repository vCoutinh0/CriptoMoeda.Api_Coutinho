﻿namespace DbRepositoryAdapter.Entidades
{
    public class NegociacoesHistoricoDbModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Sigla da criptomoeda
        /// </summary>
        public string Sigla { get; set; }

        /// <summary>
        /// Maior preço unitário de negociação das últimas 24 horas.
        /// </summary>
        public decimal MaiorPreco { get; set; }

        /// <summary>
        /// Menor preço unitário de negociação das últimas 24 horas.
        /// </summary>
        public decimal MenorPreco { get; set; }

        /// <summary>
        /// Quantidade negociada nas últimas 24 horas.
        /// </summary>
        public decimal QuantidadeNegociada { get; set; }

        /// <summary>
        /// Preço unitário da última negociação.
        /// </summary>
        public decimal PrecoUnitario { get; set; }

        /// <summary>
        /// Maior preço de oferta de compra das últimas 24 horas.
        /// </summary>
        public decimal MaiorPrecoOfertado { get; set; }

        /// <summary>
        /// Menor preço de oferta de venda das últimas 24 horas.
        /// </summary>
        public decimal MenorPrecoOfertado { get; set; }

        /// <summary>
        /// Data e hora da informação em Era Unix.
        /// </summary>
        public DateTime DataHora { get; set; }
    }
}
