namespace HealthFlow.Models
{
    public class Itemm
    {
        public int IdItem { get; set; }

        public double Quantidade { get; set; }

        public double ValorUnitario { get; set; }

        public int FkIdServico { get; set; }

        public int FkIdVenda { get; set; }
    }
}
