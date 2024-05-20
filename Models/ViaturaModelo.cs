using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;  // Certifique-se de ter a biblioteca instalada

namespace Kutuka.Models
{
    public class ViaturaModelo
    {
        [Key]
        public int Id_Viatura { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Marca { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Modelo { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Cor { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int AnoFabricacao { get; set; }

        public string Descricao { get; set; }

        public string Imagens { get; set; }

        // Propriedade de navegação para participações associadas a esta viatura
        [JsonIgnore] // Ignora essa propriedade durante a serialização JSON
        public virtual ICollection<ParticipacaoModelo> Participacoes { get; set; }
    }

    public class ViaturaCriacaoModel
    {
        [JsonProperty(Required = Required.Always)]
        public string Marca { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Modelo { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Cor { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int AnoFabricacao { get; set; }

        public string Descricao { get; set; }

        public string Imagens { get; set; }
    }

    public class ViaturaEdicaoModel
    {
        [JsonProperty(Required = Required.Always)]
        public string Marca { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Modelo { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Cor { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int AnoFabricacao { get; set; }

        public string Descricao { get; set; }

        public string Imagens { get; set; }
    }
}
