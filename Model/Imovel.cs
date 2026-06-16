using Core.Enums;

namespace Model
{
    public class Imovel
    {
        #region Propriedades
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Enums.Categoria Categoria { get; set; }
        public Enums.TipoNegocio TipoNegocio { get; set; }
        public float Valor { get; set; }
        public Endereco Endereco { get; set; }
        public string Especificacoes { get; set; }
        public string? FotoUrl { get; set; }
        #endregion
        #region Construtores
        public Imovel()
        {

        }
        public Imovel (
            int id,
            string titulo,
            string descricao,
            Enums.Categoria categoria,
            Enums.TipoNegocio tipoNegocio,
            float valor,
            Endereco endereco,
            string especificacoes,
            string? fotoUrl
        )
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Categoria = categoria;
            TipoNegocio = tipoNegocio;
            Valor = valor;
            Endereco = endereco;
            Especificacoes = especificacoes;
            FotoUrl = fotoUrl;
        }
        #endregion
    }
}
