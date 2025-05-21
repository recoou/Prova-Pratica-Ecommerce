# Instruções para fazer a Excução do Código.
---
## Especificações do Projeto:

O projeto foi feito utilizando dotnet 9, o banco de dados PostgrelSql, se baseando em uma arquitetura em camadas. Ao invés de utilizar o swagger, eu utilizei um outro pacote nuget chamado Scalar para fazer a documentação dos endpoints e fornecer uma interface de testes. Explicarei como utilizar isso nos próximos tópicos.

---

## Antes de Executar o Código:

Primeiro: Faça o clone do repositório em uma pasta separada no seu computador local e abra a solução utilizando a versão mais recente do Visual Studio. Certifique-se também de ter o dotnet 9 instalado na sua máquina.

Segundo: Configure um novo banco de dados PostgreSQL na sua máquina local, você pode utilizar o PGAdmin para isso. A connection string para o banco de dados encontra-se dentro do arquivo appsettings.json, que está na raiz da solução Ecommerce. Atenção especial para a porta, que deve ser confirado para o valor 5433.

Terceiro: Execute as migrations do banco de dados. Para tal, no console do gerenciador de pacotes, primeiro crie uma nova migration com o comando: Add-Migration "NomeDaMigration". Depois, execute as migrations utilizando o comando: Update-Database.

---

## Executando a Aplicação:

Para executar a aplicação, clique no botão de play no topo do Visual Studio com o texto de https. Se tudo ocorrer bem, o Visual Studio entrará em modo debug e uma janela do cmd será aberta.

Enquanto a aplicação estiver executando, acesse o link "https://localhost:7163/scalar/" para ter acesso à interface do Scalar, de modo que você possa ver os endpoints disponíveis. Atenção a um detalhe, talvez a porta mude, então talvez o valor não seja 7163. O valor dessa porta, se tiver mudado, deve estar no arquivo launchSettings dentro da pasta Properties, no profile "https".

Seja utilizando a interface do Scalar ou utilizando um outro programa como Insomnia ou Postman, sinta-se livre para testar os endpoints como quiser.

---

## Executando Testes Unitários:

O projeto conta com testes unitários para algumas das funções da camada de persistência, presentes dentro da pasta Tests. para executá-los, abra um console dentro do Visual Studio e execute o comando: dotnet test.

---

# 🧪 Prova Prática

Bem-vindo(a)! Esta é sua prova prática para a vaga de Desenvolvedor .NET. A ideia é simular um desafio realista do dia a dia de desenvolvimento.

---

## 📦 Desafio: Cadastro e Consulta de Produtos

Você deverá desenvolver uma API REST para gerenciamento de produtos. Essa API será usada para manter o catálogo de produtos de um e-commerce.

### Funcionalidades obrigatórias:

- Cadastrar um novo produto
- Editar produto existente
- Excluir um produto
- Consultar lista de produtos com filtros:
  - Por categoria
  - Por faixa de preço
  - Por status (Ativo/Inativo)
  - Upload de imagem do produto
  - Simular envio para a AWS S3 (pode ser salvo em disco ou usar MinIO local)

---

## 🛠️ Requisitos Técnicos

- .NET 6 ou superior
- API REST
- Usar alguma arquitetura, por exemplo em camadas
- Banco de dados relacional (PostgreSQL)
- Documentando os endpoints, por exemplo com o Swagger
- Testes em pelo menos uma parte da regra de negócio

---

## ✨ Diferenciais (Bônus)

Estes itens não são obrigatórios, mas contam pontos na avaliação:

- CI/CD (ex: GitHub Actions para build/test)
- Diagrama da arquitetura ou documentação da estrutura do código
- Docker (com docker-compose subindo app e banco)

---

## ✅ Critérios de Avaliação

- Clareza e organização do código
- Uso adequado de OOP e boas práticas (SOLID, Clean Code)
- Estrutura dos endpoints e convenções REST
- Cobertura e qualidade dos testes
- Commits claros e bem organizados
- Facilidade de execução do projeto

---

## 🚀 Como Entregar

1. Faça um **fork deste repositório** ou clone e crie um repositório público seu.
2. Desenvolva a prova no seu repositório.
3. Inclua no seu README instruções claras para rodar o projeto localmente.
4. Quando finalizar, envie o link do seu repositório para a pessoa responsável pelo processo.

---

## ⏰ Prazo

Você terá **3 à 5 dias úteis** para entregar a prova a partir da data de recebimento. Se precisar de mais tempo, avise!

---

Boa sorte! 💻🚀
