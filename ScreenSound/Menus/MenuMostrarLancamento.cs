using ScreenSound.Banco;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Menus
{
    internal class MenuMostrarLancamento : Menu
    {
        public override void Executar(DAL<Artista> artistaDAL)
        {
            base.Executar(artistaDAL);
            ExibirTituloDaOpcao("Exibindo todos as músicas registradas a partir de uma data");
            Console.Write("Digite o ano do lançamento das músicas: ");
            string anoLancamento = Console.ReadLine()!;
            var musicaDAL = new DAL<Musica>(new ScreenSoundContext());
            Func<Musica, bool> condicao = m => m.AnoLancamento.Equals(Convert.ToInt32(anoLancamento));
            
            var musicas = musicaDAL.ListarPor(condicao);
            if (musicas is not null)
            {
                foreach (var musica in musicas)
                {
                    Console.WriteLine($"Nome:{musica.Nome}");
                }
                Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"\nO ano {anoLancamento} não foi encontrada!");
                Console.WriteLine("Digite uma tecla para voltar ao menu principal");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
