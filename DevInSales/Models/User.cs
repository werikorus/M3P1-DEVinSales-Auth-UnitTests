using System.ComponentModel.DataAnnotations.Schema;
using DevInSales.Enums;
using Microsoft.OpenApi.Extensions;

namespace DevInSales.Models;

public class User
{
    [Column("id")]
    public int Id { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("UserName")]
    public string UserName { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("birth_date")]
    public DateTime BirthDate { get; set; }
    [Column("permition")]
    public Permitions Permition { get; set; }
    [Column("role")]
    public string Role { get; set; }
    public string DescricaoPermissao => Permition.GetDisplayName();

    public Profile Profile { get; set; }
    public int ProfileId { get; set; }

    public User()
    {
    }
}