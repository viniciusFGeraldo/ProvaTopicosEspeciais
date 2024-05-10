using Microsoft.EntityFrameworkCore;

namespace ThiagoSchwantesDeMoura.models;

public class Folha
{
    public int FolhaId { get; set; }
    public double Valor { get; set; } = 0;
    public int Quantidade { get; set; } = 0;
    public int Mes { get; set; }
    public int Ano { get; set; }

    public double SalarioBruto { get ; set; }
    public double ImpostoIrrf { get; set; }
    public double ImpostoInss { get; set; }
    public double ImpostoFgts { get; set; }
    public double SalarioLiquido { get; set; }
    

    public int? FuncionarioId { get; set; }
    public Funcionario? Funcionario { get; set; }
}