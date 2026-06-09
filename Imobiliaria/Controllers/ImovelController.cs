using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

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

        public IActionResult Index()
        {
            var imoveis = _imovelRepository.GetAll();
            return View(imoveis);
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
