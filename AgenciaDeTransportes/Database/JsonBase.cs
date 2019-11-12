using System.IO;
using AgenciaDeTransportes.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace AgenciaDeTransportes.Database
{
    public class JsonBase
    {
        public string CaminhoArquivo { get; set; } = $@"{Directory.GetCurrentDirectory()}\base.json";
        public Agencia BuscarDados()
        {
            if (!File.Exists(CaminhoArquivo))
                return null;

            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

            JObject Arquivo = JObject.Parse(File.ReadAllText(CaminhoArquivo));
            Agencia agencia = JsonConvert.DeserializeObject<Agencia>(Arquivo.ToString(), settings);
            return agencia;
        }
        public void Salvar(Agencia agencia)
        {
            using (StreamWriter arquivo = File.CreateText(CaminhoArquivo))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                string ArquivoJson = JsonConvert.SerializeObject(agencia, settings);
                arquivo.WriteLine(ArquivoJson);
            }
        }

    }
}
