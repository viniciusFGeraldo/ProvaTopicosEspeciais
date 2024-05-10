using System.Diagnostics.CodeAnalysis;

namespace ThiagoSchwantesDeMoura.models;

public class Funcionario
{
    public int FuncionarioId { get; set; }
    public string Nome { get; set; } = null!;
    public string Cpf { get; set; } = null!;

    public ICollection<Folha>? Folhas { get; set;}
}