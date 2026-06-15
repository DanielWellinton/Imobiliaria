using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using Microsoft.AspNetCore.Hosting; // Necessário para IWebHostEnvironment
using Microsoft.AspNetCore.Http;    // Necessário para IFormFile
using System.IO;

namespace Imobiliaria.Controllers
{
    public class ImovelController : Controller
    {
        private readonly ImovelRepository _imovelRepository;
        private readonly EnderecoRepository _enderecoRepository;
        private readonly IWebHostEnvironment _environment; // Gerencia os caminhos do servidor (wwwroot)

        // Atualizamos o construtor para receber o IWebHostEnvironment por injeção de dependência
        public ImovelController(IWebHostEnvironment environment)
        {
            _imovelRepository = new ImovelRepository();
            _enderecoRepository = new EnderecoRepository();
            _environment = environment;
        }

        public IActionResult Index(int? categoria, int? tipoNegocio, string busca, float? precoMin, float? precoMax)
        {
            var imoveis = _imovelRepository.GetAll();
            imoveis = AplicarFiltrosPadrao(imoveis, categoria, tipoNegocio, busca, precoMin, precoMax);
            return View(imoveis);
        }

        public IActionResult Catalogo(int? categoria, int? tipoNegocio, string busca, float? precoMin, float? precoMax)
        {
            var imoveis = _imovelRepository.GetAll();
            imoveis = AplicarFiltrosPadrao(imoveis, categoria, tipoNegocio, busca, precoMin, precoMax);
            return View(imoveis);
        }

        private List<Imovel> AplicarFiltrosPadrao(List<Imovel> imoveis, int? categoria, int? tipoNegocio, string busca, float? precoMin, float? precoMax)
        {
            if (imoveis == null) return new List<Imovel>();

            if (categoria.HasValue)
                imoveis = imoveis.Where(x => (int)x.Categoria == categoria.Value).ToList();

            if (tipoNegocio.HasValue)
                imoveis = imoveis.Where(x => (int)x.TipoNegocio == tipoNegocio.Value).ToList();

            if (precoMin.HasValue)
                imoveis = imoveis.Where(x => x.Valor >= precoMin.Value).ToList();

            if (precoMax.HasValue)
                imoveis = imoveis.Where(x => x.Valor <= precoMax.Value).ToList();

            if (!string.IsNullOrEmpty(busca))
            {
                imoveis = imoveis.Where(x =>
                    x.Titulo.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
                    x.Descricao.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
                    (x.Endereco != null && x.Endereco.Cidade.Contains(busca, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            return imoveis;
        }

        public IActionResult Details(int id)
        {
            if (id <= 0)
                return BadRequest();

            var imovel = _imovelRepository.GetById(id);
            if (imovel is null)
                return NotFound();

            return View(imovel);
        }

        public IActionResult Create()
        {
            return View(new Imovel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Imovel imovel, IFormFile fotoArquivo)
        {
            if (imovel is null)
                return View(imovel);

            if (ModelState.IsValid)
            {
                if (fotoArquivo != null && fotoArquivo.Length > 0)
                {
                    string nomeUnicoFoto = await SalvarFotoServidor(fotoArquivo);
                    imovel.FotoUrl = "/imagens/imoveis/" + nomeUnicoFoto;
                }
            }

            _enderecoRepository.Create(imovel.Endereco);
            _imovelRepository.Create(imovel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();
            var imovel = _imovelRepository.GetById(id);
            if (imovel is null)
                return NotFound();
            return View(imovel);
        }

        public IActionResult ConfirmDelete(int id)
        {
            if (id <= 0)
                return BadRequest();
            var imovel = _imovelRepository.GetById(id);
            if (imovel is null)
                return NotFound();

            _imovelRepository.Delete(imovel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            if (id <= 0)
                return BadRequest();
            var imovel = _imovelRepository.GetById(id);
            if (imovel is null)
                return NotFound();
            return View(imovel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Imovel imovel, IFormFile fotoArquivo)
        {
            if (id <= 0 || imovel is null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                if (fotoArquivo != null && fotoArquivo.Length > 0)
                {
                    string nomeUnicoFoto = await SalvarFotoServidor(fotoArquivo);
                    imovel.FotoUrl = "/imagens/imoveis/" + nomeUnicoFoto;
                }
                else
                {
                    var imovelAntigo = _imovelRepository.GetById(id);
                    if (imovelAntigo != null)
                    {
                        imovel.FotoUrl = imovelAntigo.FotoUrl;
                    }
                }
            }

            _enderecoRepository.Update(imovel.Endereco);
            _imovelRepository.Update(imovel);
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SalvarFotoServidor(IFormFile arquivo)
        {
            string extensao = Path.GetExtension(arquivo.FileName);
            string nomeUnico = Guid.NewGuid().ToString() + extensao;

            string pastaDestino = Path.Combine(_environment.WebRootPath, "imagens", "imoveis");

            if (!Directory.Exists(pastaDestino))
                Directory.CreateDirectory(pastaDestino);

            string caminhoCompletoArquivo = Path.Combine(pastaDestino, nomeUnico);

            using (var stream = new FileStream(caminhoCompletoArquivo, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return nomeUnico;
        }
    }
}