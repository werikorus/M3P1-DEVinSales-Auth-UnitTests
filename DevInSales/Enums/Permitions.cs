using System.ComponentModel.DataAnnotations;

namespace DevInSales.Enums
{
    public enum Permitions
    {
        [Display(Name = "Funcionário")]
        Funcionario = 1,
        [Display(Name = "Gerente")]
        Gerente = 2,
        [Display(Name = "Administrador")]
        Administrador
    }
}
