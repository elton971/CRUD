//criacao das classes que serviram como dependecia.

using Microsoft.AspNetCore.Identity;

namespace Doiman
{
    public class User
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
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
//$dotnet watch run => correr a aplicacao -executamos na pasta de API

//por defeito todas as tabelas tem relacao de 1-1

//para criar uma relacao podes colocar o objecto da tabela que deseja cd