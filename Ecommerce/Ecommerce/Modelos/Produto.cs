namespace Ecommerce.Modelos
{
    public class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Categoria { get; set; }
        public double? Preco { get; set; }
        public bool Status { get; set; } = true;
        public string? ImagemCaminho { get; set; }
    }
}
