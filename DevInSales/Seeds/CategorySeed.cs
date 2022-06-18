using DevInSales.Models;
using System.Diagnostics.CodeAnalysis;

namespace DevInSales.Seeds
{
    [ExcludeFromCodeCoverage]
    public class CategorySeed
    {
        public static List<Category> Seed { get; set; } = new List<Category>() { new Category(1, "Categoria Padrão", "categoria-padrao")};
    }
}
