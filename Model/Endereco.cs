using Core.Enums;
namespace Model
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public Enums.EstadoFederativo Estado { get; set; }
        public string Cep { get; set; }
        public int? Numero { get; set; }
        public string Complemento { get; set; }

        public Endereco()
        {
        }

        public Endereco(
            int id, 
            string rua, 
            string bairro, 
            string cidade, 
            Enums.EstadoFederativo estado, 
            string cep, 
            int? numero, 
            string complemento
        )
        {
            Id = id;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
            Numero = numero;
            Complemento = complemento;
        }
    }
}
