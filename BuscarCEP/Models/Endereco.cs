using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuscarCEP.Models
{
    [Table("CEP")]
    public class Endereco
    {
        [Key]
        public int id { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string uf { get; set; }
        public long? unidade { get; set; }
        public int ibge { get; set; }
        public string gia { get; set; }

        public Endereco(string cep, string logradouro, string bairro, string uf, long? unidade, int ibge, string gia)
        {
            this.cep = cep;
            this.logradouro = logradouro;
            this.bairro = bairro;
            this.uf = uf;
            this.unidade = unidade;
            this.ibge = ibge;
            this.gia = gia;
        }

    }
}
