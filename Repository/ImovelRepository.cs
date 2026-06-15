using Model;
using Repository.VirtualDataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ImovelRepository
    {
        public void Create(Imovel imovel)
        {
            imovel.Id = GetNextId();
            MyData.Imoveis.Add(imovel);
        }

        public void Update(Imovel imovel)
        {
            var _imovel = GetById(imovel.Id);
            _imovel.Titulo = imovel.Titulo;
            _imovel.Descricao = imovel.Descricao;
            _imovel.Categoria = imovel.Categoria;
            _imovel.TipoNegocio = imovel.TipoNegocio;
            _imovel.Valor = imovel.Valor;
            _imovel.Endereco = imovel.Endereco;
            _imovel.Especificacoes = imovel.Especificacoes;
            _imovel.FotoUrl = imovel.FotoUrl;
        }

        public void Delete(Imovel imovel)
        {
            MyData.Imoveis.Remove(imovel);
        }

        public Imovel GetById(int id)
        {
            var imovel = MyData.Imoveis.FirstOrDefault(e => e.Id == id);
            if (imovel == null)
            {
                throw new Exception("Imovel não encontrado");
            }
            return imovel;
        }

        public List<Imovel> GetAll()
        {
            return MyData.Imoveis;
        }

        private int GetNextId()
        {
            int maxId = 0;
            foreach (var imovel in MyData.Imoveis)
            {
                if (imovel.Id > maxId)
                {
                    maxId = imovel.Id;
                }
            }
            return ++maxId;
        }
    }
}
