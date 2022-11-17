//criacao das classes que serviram como dependecia.

using Microsoft.AspNetCore.Identity;

namespace Doiman
{
    public class User:IdentityUser
    {
        
        public string FullName { get; set; }
     
    }
}

//primeiro criaamo as classes no domian
//segundo criamos o DataContext na canada de persistencia
//depois criamos uma pasta na aplicacao chamada Post e criamos uma classe create post.
//depois vamos registar tudo no controllar.
//criamos o baseCotroller/postcotroller
//depois fazemos a confiuracao de servicos
//vamos ajustar o ficheiro appsettings.json, e colocamos o connection String
//depois criamos os scrips para configuracao da base de dados(como criar tabelas) vamos no terminal
//$dotnet ef migrations add initialMigration -p Persistence --context DataContext
//$dotnet ef database update -p Persistence -s APi =>Vai  atualizar a dadabase
//dotnet ef migrations add addUserOnPost -p Persistence -s API/
//$dotnet watch run => correr a aplicacao -executamos na pasta de API

//por defeito todas as tabelas tem relacao de 1-1

//para criar uma relacao podes colocar o objecto da tabela que deseja cd

//DTO - Compia uma nova classe sem as informacoes  sencievis  da tabela->camada de aplicacao(sempre deve vir Id)

//AutoMapper.Extensions.Microsoft.DependencyInjection na camada de API
//AutoMapper na camada de aplication

//para criar uma automacao temos que criar uma classe exeplo na camada de aplicacao helpers/mappingProfiles
//so fazemos mapeamento so quando tempos chaves estrageiras

//gestao dos dados do  usuario usamos Microsoft.AspNetCore.Identity.EntityFrameworkCore  instalamos na camada de domain
//vai servir para a seguranca do utilizar.
//1-baixamos os pacotes de rotas nuget packages
//ORM (ENTITY FRAMAWORK)->CRIA AS TABELAS DA BASE DE DADOS  na camada de Api
//tudo de base de dados instalamos na camada de persistencia

//npgsql vais gerira cumincacao com a cuminicacao com a base de dados e instalamos na camada de API
//Npgsql.EntityFrameworkCore.PostgreSQL Vai intregar o postgree com o etntity framework instalamos no API e na persistencia
//Microsoft.EntityFrameworkCore.Design instalamos na camada de API.
//Microsoft.Extensions.Configuration instalamos na camada de aplicacao, vai ajudar na injecao de dependencia.
//FluentValidation.AspNetCore vai servir para a validadaco das requisicoes, instalamos na camada de aplicacao.
//MediatR.Extensions.Microsoft.DependencyInjection camada de API e APlicacao serve para gestao de requisisces
//Newtonsoft.Json processamento de dados tipo json camada de API



//Mildlewares-é onde vamos colocar todas as informacoes de erro

//JWT-vai funcionar para a auteticacao.
//Microsoft.AspNetCore.Authentication.JwtBearer-instalamos na versoa de API-x
//System.IdentityModel.Tokens.Jwt-responsavel pela verificacao de token camada de infra
//Microsoft.IdentityModel.Tokens-camada de infras..