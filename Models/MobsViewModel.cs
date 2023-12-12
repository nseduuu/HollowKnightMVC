using RpgMvc.Models;

namespace RpgMvc.Models
{
    public class MobsViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? PontosVida { get; set; }
        public int? Forca { get; set; }
        public string? Dificuldade { get; set; }
        public string? Localizacao { get; set; }
        public string? Lore { get; set; }
        public string? Foto {get; set;}
        
        // public List<Item> MobItens { get; set; }
        // public CategoriaEnum Categoria { get; set;}  

        // public List<PersonagemHabilidadeViewModel> PersonagemHabilidades {get; set;}
      
    }
}