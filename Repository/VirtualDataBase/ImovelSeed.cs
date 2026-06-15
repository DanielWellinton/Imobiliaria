using Core.Enums;
using Model;

namespace Repository.VirtualDataBase
{
    public static class ImovelSeed
    {
        public static void SeedData()
        {
            var imovelRepository = new ImovelRepository();
            var enderecoRepository = new EnderecoRepository();

            // Evita duplicar os dados se já houver registros no banco
            if (imovelRepository.GetAll()?.Count > 0)
            {
                return;
            }

            // Lista de endereços de teste
            var enderecos = new List<Endereco>
        {
            new Endereco(0, "Av. Paulista", "Bela Vista", "São Paulo", Enums.EstadoFederativo.SP, "01311-200", 1200, "Apto 142 - Bloco B"),
            new Endereco(0, "Rua das Flores", "Centro", "Gramado", Enums.EstadoFederativo.RS, "95670-000", 350, "Casa"),
            new Endereco(0, "Av. Atlântica", "Copacabana", "Rio de Janeiro", Enums.EstadoFederativo.RJ, "22021-001", 2500, "Cobertura"),
            new Endereco(0, "Rodovia BR-282, Km 45", "Zona Rural", "Lages", Enums.EstadoFederativo.SC, "88500-000", 0, "Sítio São José"),
            new Endereco(0, "Rua Amélia", "Graças", "Recife", Enums.EstadoFederativo.PE, "52011-050", 410, "Sala 302 - Empresarial")
        };

            // Lista de imóveis de teste correspondentes
            var imoveis = new List<Imovel>
        {
            new Imovel
            {
                Titulo = "Apartamento Compacto na Av. Paulista",
                Descricao = "Excelente oportunidade para quem quer morar no coração de SP. Próximo ao metrô Trianon-Masp, portaria 24h e academia completa no condomínio.",
                Categoria = Enums.Categoria.Residencial,
                TipoNegocio = Enums.TipoNegocio.Aluguel,
                Valor = 3200.00f,
                Especificacoes = "1 Quarto, 1 Banheiro, 45m², Sem vaga de garagem"
            },
            new Imovel
            {
                Titulo = "Casa de Campo Charmosa em Gramado",
                Descricao = "Linda casa estilo rústico europeu com lareira, calefação instalada e um amplo quintal arborizado perfeito para os dias de inverno.",
                Categoria = Enums.Categoria.Residencial,
                TipoNegocio = Enums.TipoNegocio.Venda,
                Valor = 850000.00f,
                Especificacoes = "3 Quartos (1 Suíte), 2 Banheiros, 180m², 2 Vagas"
            },
            new Imovel
            {
                Titulo = "Cobertura Duplex Vista Mar Copacabana",
                Descricao = "Exclusiva cobertura com piscina privativa, churrasqueira e uma vista panorâmica deslumbrante para a praia de Copacabana.",
                Categoria = Enums.Categoria.Residencial,
                TipoNegocio = Enums.TipoNegocio.Venda,
                Valor = 4200000.00f,
                Especificacoes = "4 Suítes, 5 Banheiros, 320m², 3 Vagas"
            },
            new Imovel
            {
                Titulo = "Fazenda Produtiva com Lavouras e Gado",
                Descricao = "Propriedade rural rica em água, com nascente, casa sede centenária reformada, mangueira para gado e solo ideal para plantio de grãos.",
                Categoria = Enums.Categoria.Rural,
                TipoNegocio = Enums.TipoNegocio.Venda,
                Valor = 1500000.00f,
                Especificacoes = "50 Hectares, Casa Sede, Galpão de Maquinário"
            },
            new Imovel
            {
                Titulo = "Sala Comercial em Prédio Corporativo",
                Descricao = "Sala pronta para escritório ou consultório. Piso elevado em granito, teto rebaixado em gesso, iluminação LED e infraestrutura para ar-condicionado.",
                Categoria = Enums.Categoria.Comercial,
                TipoNegocio = Enums.TipoNegocio.Aluguel,
                Valor = 1800.00f,
                Especificacoes = "1 Sala Principal, 1 Banheiro, 35m², 1 Vaga rotativa"
            }
        };

            // Processo de gravação amarrando as duas entidades
            for (int i = 0; i < imoveis.Count; i++)
            {
                var enderecoAtual = enderecos[i];
                var imovelAtual = imoveis[i];

                // 1. Cadastra o endereço primeiro
                enderecoRepository.Create(enderecoAtual);

                // 2. Associa o endereço (já com o ID populado se o banco for auto-incremento) ao imóvel
                imovelAtual.Endereco = enderecoAtual;

                // 3. Cadastra o imóvel no banco
                imovelRepository.Create(imovelAtual);
            }
        }
    }
}
