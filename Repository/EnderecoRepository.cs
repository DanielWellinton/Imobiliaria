using Model;
using Repository.VirtualDataBase;

namespace Repository
{
    public class EnderecoRepository
    {
        public void Create(Endereco endereco)
        {
            endereco.Id = GetNextId();
            MyData.Enderecos.Add(endereco);
        }

        public void Update(Endereco endereco)
        {
            var _endereco = GetById(endereco.Id);
            _endereco.Rua = endereco.Rua;
            _endereco.Bairro = endereco.Bairro;
            _endereco.Cidade = endereco.Cidade;
            _endereco.Estado = endereco.Estado;
        }

        public void Delete(Endereco endereco)
        {
            MyData.Enderecos.Remove(endereco);
        }

        public Endereco GetById(int id)
        {
            var endereco = MyData.Enderecos.FirstOrDefault(e => e.Id == id);
            if (endereco == null)
            {
                throw new Exception("Endereço não encontrado");
            }
            return endereco;
        }

        public List<Endereco> GetAll()
        {
            return MyData.Enderecos;
        }

        private int GetNextId()
        {
            int maxId = 1;
            foreach (var endereco in MyData.Enderecos)
            {
                if (endereco.Id > maxId)
                {
                    maxId = endereco.Id;
                }
            }
            return maxId;
        }
    }
}
