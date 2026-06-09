using Core.Enums;

namespace Model
{
    public class Imovel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Enums.Categoria Categoria { get; set; }
        public Enums.TipoNegocio TipoNegocio { get; set; }
        public float Valor { get; set; }
        public Endereco Endereco { get; set; }
        public string Especificacoes { get; set; }
    }
}
