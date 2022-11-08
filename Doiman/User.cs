//criacao das classes que serviram como dependecia.

using Microsoft.AspNetCore.Identity;

namespace Doiman
{
    public class User:IdentityUser//ja vem com  atributos para o user
    {
       
        public string Fullname { get; set; }
       
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