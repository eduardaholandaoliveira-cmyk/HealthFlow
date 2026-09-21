namespace HealthFlow.Models
{
    public class Servicom
    {
        public int IdServico { get; set; }

        public string Nome { get; set; } = string.Empty;

        public double ValorEstimado { get; set; }

        public int GarantiaMeses { get; set; }

        public TimeSpan TempoEstimado { get; set; }
    }
}
