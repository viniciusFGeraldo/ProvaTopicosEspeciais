using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThiagoSchwantesDeMoura.models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.MapPost("/funcionario/cadastrar", ([FromBody] Funcionario funcionario, [FromServices] AppDbContext ctx) =>
{
     Funcionario? Funcionario = ctx.Funcionarios.FirstOrDefault(f => f.Cpf == funcionario.Cpf);

    if(Funcionario is not null){
        return Results.NotFound("Já existe um Funcionario cadastrado com esse cpf");
    }

    ctx.Funcionarios.Add(funcionario);
    ctx.SaveChanges();

    return Results.Created("", funcionario);
});


app.MapGet("/funcionario/listar", ([FromServices] AppDbContext ctx) =>
{
    List<Funcionario>? Funcionarios = ctx.Funcionarios.ToList();

    if(Funcionarios is not null){
        return Results.Ok(Funcionarios);
    }

    return Results.NotFound("Nenhum funcionário cadastrado");   
});

app.MapPost("/folha/cadastrar", ([FromBody] Folha folha, [FromServices] AppDbContext ctx) =>
{
    Funcionario? Funcionario = ctx.Funcionarios.FirstOrDefault(f => f.FuncionarioId == folha.FuncionarioId);

    if(Funcionario is null){
        return Results.NotFound("Funcionario não encontrado");
    }
    
    Folha? Folha = ctx.Folhas.Include(f => f.Funcionario).FirstOrDefault(
        f => (f.Funcionario.FuncionarioId == folha.FuncionarioId) && (f.Mes == folha.Mes) && (f.Ano == folha.Ano)
    );

    if(Folha is not null){
        return Results.NotFound("O funcionário já possui a folha de pagamento do mes: " + folha.Mes + "/" + folha.Ano);
    }

    folha.SalarioBruto = folha.Quantidade * folha.Valor;

    //Imposto de Renda
    if(folha.SalarioBruto <= 1903.98){
        folha.ImpostoIrrf = 0;
    }else if((folha.SalarioBruto > 1903.98) && (folha.SalarioBruto <= 2826.65)){
        folha.ImpostoIrrf = folha.SalarioBruto * 7.5 / 100;
    }else if((folha.SalarioBruto > 2826.65) && (folha.SalarioBruto <= 3751.05)){
        folha.ImpostoIrrf = folha.SalarioBruto * 15.0 / 100;
    }else if((folha.SalarioBruto >  3751.05) && (folha.SalarioBruto <= 4664.68)){
        folha.ImpostoIrrf = folha.SalarioBruto * 22.5 / 100;
    }else if(folha.SalarioBruto > 4664.68){
        folha.ImpostoIrrf = folha.SalarioBruto * 27.5 / 100;
    }

    //imposto INSS
    if(folha.SalarioBruto <= 1693.72){
        folha.ImpostoInss = folha.SalarioBruto * 8 / 100;
    }else if((folha.SalarioBruto > 1693.72) && (folha.SalarioBruto <= 2822.90)){
        folha.ImpostoInss = folha.SalarioBruto * 9 / 100;
    }else if((folha.SalarioBruto > 2822.90) && (folha.SalarioBruto <= 5645.80)){
        folha.ImpostoInss = folha.SalarioBruto * 11 / 100;
    }else if(folha.SalarioBruto > 5645.80){
        folha.ImpostoInss = 621.03;
    }

    folha.ImpostoFgts =  folha.SalarioBruto * 8 / 100;
    folha.SalarioLiquido = folha.SalarioBruto - folha.ImpostoIrrf - folha.ImpostoInss;

    Funcionario.Folhas?.Add(folha);
    ctx.Funcionarios.Update(Funcionario);
    ctx.Folhas.Add(folha);
    ctx.SaveChanges();

    return Results.Created("", folha);
});

app.MapGet("/folha/listar", ([FromServices] AppDbContext ctx) =>
{
    List<Folha>? Folhas = ctx.Folhas.Include(f => f.Funcionario).ToList();

    if(Folhas is not null){
        return Results.Ok(Folhas);
    }

    return Results.NotFound("Nenhuma folha de pagamento cadastrada");   
});

app.MapGet("/folha/buscar/{cpf}/{mes}/{ano}", ([FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano, [FromServices] AppDbContext ctx) =>
{

    Funcionario? Funcionario = ctx.Funcionarios.Include(f => f.Folhas).FirstOrDefault(f => f.Cpf == cpf);

    if(Funcionario is null){
        return Results.NotFound("Funcionario não encontrado");
    }

    Folha? Folha = ctx.Folhas.Include(f => f.Funcionario).FirstOrDefault(
        f => (f.Funcionario.Cpf == cpf) && (f.Mes == mes) && (f.Ano == ano)
    );

    if(Folha is null){
        return Results.NotFound("Folha não encontrada");
    }


    return Results.Ok(Folha);   
});


app.Run();
