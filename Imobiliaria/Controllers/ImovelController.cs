using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using System.Reflection.Metadata.Ecma335;
using static Core.Enums.Enums;

namespace Imobiliaria.Controllers
{
    public class ImovelController : Controller
    {
        private ImovelRepository _imovelRepository;
        private EnderecoRepository _enderecoRepository;

        public ImovelController()
        {
            _imovelRepository = new ImovelRepository();
            _enderecoRepository = new EnderecoRepository();
        }

        public IActionResult Index(int? categoria, int? tipoNegocio, string busca, float? precoMin, float? precoMax)
        {
            var imoveis = _imovelRepository.GetAll();

            imoveis = FiltrarImoveis(imoveis, categoria, tipoNegocio, busca, precoMin, precoMax);

            return View(imoveis);
        }

        public IActionResult Catalogo(int? categoria, int? tipoNegocio, string busca, float? precoMin, float? precoMax)
        {
            var imoveis = _imovelRepository.GetAll();

            imoveis = FiltrarImoveis(imoveis, categoria, tipoNegocio, busca, precoMin, precoMax);

            return View(imoveis);
        }

        private List<Imovel> FiltrarImoveis(List<Imovel> imoveis, int? categoria, int? tipoNegocio, string busca, float? precoMin, float? precoMax)
        {
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
                imoveis = imoveis.Where(x => x.Titulo.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
                                             (x.Endereco != null && x.Endereco.Cidade.Contains(busca, StringComparison.OrdinalIgnoreCase))).ToList();
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
        public IActionResult Create(Imovel imovel)
        {
            if (imovel is null)
                return View(imovel);
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
        public IActionResult Update(int id, Imovel imovel)
        {
            if (id <= 0)
                return BadRequest();
            if (imovel is null)
                return BadRequest();
            _enderecoRepository.Update(imovel.Endereco);
            _imovelRepository.Update(imovel);
            return RedirectToAction(nameof(Index));
        }
    }
}
