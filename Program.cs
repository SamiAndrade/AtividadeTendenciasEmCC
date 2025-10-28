using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Lista de animais com caminho relativo das imagens
        List<Animal> animais = new List<Animal>
        {
            new Animal { Nome = "Cachorro", ImagemPath = @"img\cachorro.jpg" },
            new Animal { Nome = "Raposa", ImagemPath = @"img\raposa.png" },
            new Animal { Nome = "Papagaio", ImagemPath = @"img\papagaio.jpg" },
            new Animal { Nome = "Lemure", ImagemPath = @"img\lemure.jpg" },
            new Animal { Nome = "Tartaruga", ImagemPath = @"img\tartaruga.jpg" },
            new Animal { Nome = "Baiacu", ImagemPath = @"img\baiacu.jpg" },
            new Animal { Nome = "Sucuri", ImagemPath = @"img\sucuri.jpg" }
        };

        while (true)
        {
            Console.WriteLine("\nDigite o nome ou parte do nome do animal que deseja buscar, 'listar' para ver todos os animais, ou 'sair' para encerrar:");
            string entrada = Console.ReadLine();

            if (entrada.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Encerrando o programa...");
                break;
            }

            if (entrada.Equals("listar", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nAnimais disponíveis:");
                for (int i = 0; i < animais.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {animais[i].Nome}");
                }
                continue; // volta para o loop principal
            }

            var resultados = animais
                .Where(a => a.Nome.IndexOf(entrada, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            if (resultados.Count > 0)
            {
                Console.WriteLine("\nAnimais encontrados:");
                for (int i = 0; i < resultados.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {resultados[i].Nome}");
                }

                Console.WriteLine("\nDigite o número do animal para ver a imagem (ou pressione Enter para pular):");
                string escolhaInput = Console.ReadLine();
                if (int.TryParse(escolhaInput, out int escolha) && escolha > 0 && escolha <= resultados.Count)
                {
                    string caminhoRelativo = resultados[escolha - 1].ImagemPath;
                    string caminhoAbsoluto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, caminhoRelativo);

                    if (File.Exists(caminhoAbsoluto))
                    {
                        Console.WriteLine($"Abrindo imagem de {resultados[escolha - 1].Nome}...");
                        OpenFile(caminhoAbsoluto);
                    }
                    else
                    {
                        Console.WriteLine("Imagem não encontrada: " + caminhoAbsoluto);
                    }
                }
            }
            else
            {
                Console.WriteLine("\nNenhum animal encontrado. Tente novamente.");
            }
        }
    }

    static void OpenFile(string caminho)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = caminho,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Não foi possível abrir a imagem: " + ex.Message);
        }
    }
}

class Animal
{
    public string Nome { get; set; }
    public string ImagemPath { get; set; }
}
