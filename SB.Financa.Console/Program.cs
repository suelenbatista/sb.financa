using SB.Financa.DAL;
using SB.Financa.Generics.Seguranca;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SB.Financa.Console
{
    class Program
    {
        private static string senhaCriptografada = "";
        static void Main(string[] args)
        {

            Usuario usuario = new Usuario { Login = "admin", Perfil = "ADMINISTRADOR", Senha = "1234" };

            PostLogin(usuario);

            //Service.ApiConnection.PostLogin(usuario).Wait();
            System.Console.ReadKey();

            ////String sCriptografada = Criptografia.ObterHashMd5("SB.FINANCA");


            ////System.Console.WriteLine("Iniciando os processos...");

            //System.Console.WriteLine("Gravando Pessoa Entity");
            ////GravandoPessoaEntity();

            //System.Console.WriteLine("Gravando Conta Bancaria Entity");
            ////GravandoContaBancariaEntity();

            //System.Console.WriteLine("Gravando ContaCartao Entity");
            ////GravandoContaCartaoEntity();

            //System.Console.WriteLine("Gravando Etiqueta Entity");
            ////GravandoEtiquetaEntity();

            //System.Console.WriteLine("Gravando Planejamnto Entity");
            ////GravandoPlanejamentoEntity();

            //System.Console.WriteLine("Gravando Movimento e Baixa");
            //GravandoMovimentoBaixaEntity();

            //System.Console.WriteLine("Finalizando os processos...");

            ////GravandoUsuario();
            ////CompararSenha();
            System.Console.ReadKey();
         
        }

        private static void CompararSenha()
        {
            bool  ehIgual = Criptografia.CompararHashMd5("1234", senhaCriptografada);

            System.Console.WriteLine("As senhas comparadas são iguais " + ehIgual);
        }

        private async static void PostLogin(Usuario usuario)
        {
          String token  = await Service.APILogin.PostLogin(usuario);

           if (!string.IsNullOrWhiteSpace(token))
            {
          
                List<Pessoa> pessoas =  await Service.ApiPessoa.GetPessoas(token);

                pessoas.ToList().ForEach(pessoa => 
                {
                    System.Console.WriteLine(pessoa.Nome +  " - " + pessoa.Documento);
                });
            }
        }

        private static void GravandoUsuario()
        {
            senhaCriptografada = Criptografia.ObterHashMd5("1234");

            using (var context = new FinancaContext())
            {
                Usuario usuario = new Usuario();
                usuario.Login = "admin";
                usuario.Senha = senhaCriptografada;
                //usuario.Perfil = TipoPerfil.ADMINISTRADOR;
                usuario.Perfil = "ADMINISTRADOR";

                RepositorioBaseEF<Usuario> _repo = new RepositorioBaseEF<Usuario>(context);
                _repo.Incluir(usuario);
            }
        }

        private static void GravandoMovimentoBaixaEntity()
        {
            using (var context = new FinancaContext())
            {

                ContaBancaria contaBancaria = context.ContaBancaria.First();
                ContaCartao contaCartao = context.ContaCartao.First();
                Etiqueta etiqueta = context.Etiqueta.First();
                Pessoa pessoa = context.Pessoa.First();

                Movimento newMovimento = new Movimento();
                newMovimento.Pessoa = pessoa;
                newMovimento.Etiqueta = etiqueta;
                newMovimento.Cadastro = DateTime.Now;
                newMovimento.Vencimento = DateTime.Now.AddMonths(2);
                newMovimento.Tipo = TipoMovimento.PAGAR;
                newMovimento.Status = StatusMovimento.PENDENTE;
                newMovimento.Descricao = "teste inicial";
                newMovimento.Valor = 10.00m;
                newMovimento.ValorPago = 0m;
                newMovimento.ContaCartao = contaCartao;

                MovimentoBaixa baixa1 = new MovimentoBaixa();

                baixa1.Movimento = newMovimento;
                baixa1.DataBaixa = DateTime.Now;
                baixa1.DataEvento = DateTime.Now;
                baixa1.ContaBancaria = contaBancaria;
                baixa1.Direcao = DirecaoBaixa.SAIDA;
                baixa1.ValorBaixa = 5m;

                MovimentoBaixa baixa2 = new MovimentoBaixa();

                baixa2.Movimento = newMovimento;
                baixa2.DataBaixa = DateTime.Now;
                baixa2.DataEvento = DateTime.Now;
                baixa2.ContaBancaria = contaBancaria;
                baixa2.Direcao = DirecaoBaixa.SAIDA;
                baixa2.ValorBaixa = 5m;

                List<MovimentoBaixa> lstBaixa = new List<MovimentoBaixa>();
                lstBaixa.Add(baixa1);
                lstBaixa.Add(baixa2);

                context.Movimento.Add(newMovimento);
                context.MovimentoBaixa.AddRange(lstBaixa);
                context.SaveChanges();
            }
        }

        //private static void GravandoPlanejamentoEntity()
        //{
        //    using (var context = new FinancaContext())
        //    {

        //        ContaBancaria contaBancaria = context.ContaBancaria.First();
        //        ContaCartao contaCartao = context.ContaCartao.First();
        //        Etiqueta etiqueta = context.Etiqueta.First();
        //        Pessoa pessoa = context.Pessoa.First();

        //        Planejamento planejamento = new Planejamento();
        //        planejamento.Meta = "Comprar carro novo";
        //        planejamento.DataInicial = DateTime.Now;
        //        planejamento.DataFinal = DateTime.Now.AddYears(3);
        //        planejamento.Etiqueta = etiqueta;
        //        planejamento.Tipo = TipoPlanejamento.Patrimonio;
        //        planejamento.Valor = 60000M;

        //        context.Planejamento.AddRange(planejamento);
        //        context.SaveChanges();
        //    }
        //}

        //private static void GravandoContaBancariaEntity()
        //{
        //    ContaBancaria novaContaBancaria = new ContaBancaria();
        //    novaContaBancaria.Nome = "Banco Itaú Unibanco";
        //    novaContaBancaria.Conta = "0264";
        //    novaContaBancaria.Agencia = "82872";
        //    novaContaBancaria.Digito = "4";
        //    novaContaBancaria.Ativa = true;

        //    using (var context = new FinancaContext())
        //    {
        //        context.ContaBancaria.Add(novaContaBancaria);
        //        context.SaveChanges();
        //    }
        //}

        //private static void GravandoEtiquetaEntity()
        //{
        //    Etiqueta novaEtiqueta = new Etiqueta();
        //    novaEtiqueta.Nome = "Alimentação";

        //    using (var context = new FinancaContext())
        //    {
        //        context.Etiqueta.Add(novaEtiqueta);
        //        context.SaveChanges();
        //    }
        //}

        //private static void GravandoPessoaEntity()
        //{
        //    Pessoa novaPessoa = new Pessoa();
        //    novaPessoa.Nome = "Suelen Batista da Silva";
        //    novaPessoa.Tipo = TipoDocumento.CPF;
        //    novaPessoa.Documento = "39338667820";
        //    novaPessoa.Ativo = true;

        //    using (var context = new FinancaContext())
        //    {
        //        context.Pessoa.Add(novaPessoa);
        //        context.SaveChanges();
        //    }
        //}

        //private static void GravandoContaCartaoEntity()
        //{
        //    ContaCartao novaContaCartao = new ContaCartao();

        //    novaContaCartao.Apelido = "Credcard Black";
        //    novaContaCartao.Bandeira = BandeiraCartao.Visa;
        //    novaContaCartao.Tipo = TipoCartao.Credito;

        //    using (var context = new FinancaContext())
        //    {
        //        context.ContaCartao.Add(novaContaCartao);
        //        context.SaveChanges();
        //    }
        //}
    }
}
