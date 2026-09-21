
        namespace HealthFlow.Models
    {
        public class Caixam
        {
            public int IdCaixa { get; set; }

            public DateTime Abertura { get; set; }

            public DateTime? Fechamento { get; set; }

            public double SaldoInicial { get; set; }

            public double? SaldoFinal { get; set; }

            public double TotalEntradas { get; set; }

            public double TotalSaidas { get; set; }

            public int FkIdFuncionario { get; set; }

            public string StatusCaixa { get; set; } = string.Empty;
        }
    }


