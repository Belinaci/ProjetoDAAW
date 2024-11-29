# 🎬 Site de Filmes

Um sistema web desenvolvido em **C# .NET MVC** para gerenciar e explorar informações sobre filmes, seus gêneros, artistas e personagens. 

---

## 📖 Descrição

O **Site de Filmes** é uma aplicação que permite:
- Cadastrar filmes, gêneros, artistas e personagens.
- Associar filmes aos seus gêneros.
- Associar artistas aos seus filmes.
- Associar personagens aos seus artistas
- Listar, editar e excluir filmes, gêneros, artistas e personagens.
- Pesquisar filmes por título ou gênero.
- Pesquisar artistas por nome.
- Pesquisar personagens por nome.
- Enviar Feedback para o Administrador.

O projeto foi desenvolvido utilizando o padrão MVC para organizar o código e tornar a aplicação escalável e de fácil manutenção.

---

## 🚀 Tecnologias Utilizadas

- **Linguagem:** C#  
- **Framework:** .NET 8  
- **Banco de Dados:** SQL Server suportado pelo Entity Framework  
- **ORM:** Entity Framework Core  
- **Front-End:** HTML, CSS e Razor (Motor de Views do .NET MVC)

---

## 🛠️ Funcionalidades

### 🔹 Filmes
- Cadastro de novos filmes com título, descrição, associação a um ou mais gêneros, associação a um ou mais artistas e foto.
- Edição, detalhes e exclusão de filmes.
- Listagem de todos os filmes cadastrados.
- Pesquisa por título e gênero de filmes.

### 🔹 Gêneros
- Cadastro de novos gêneros.
- Edição, detalhes e exclusão de gêneros.
- Listagem de todos os gêneros disponíveis.

### 🔹 Artistas
- Cadastro de novos artistas com nome, data de nascimento, país e foto.
- Edição, detalhes e exclusão de artistas.
- Listagem de todos os artistas cadastrados.
- Pesquisa por nome de artistas.

### 🔹 Personagens
- Cadastro de novos personagens com nome, associação com um artista e associação com um filme.
- Edição, detalhes e exclusão de personagens.
- Listagem de todos os personagens cadastrados.
- Pesquisa por nome de personagem.

### 🔹 Contato
- Cadastro de mensagens com seu nome, e-mail, telefone e mensagem.
- Somente Administrador pode ver as mensagens.

---

## 🗂️ Estrutura do Projeto

### 🖋️ Models
- `Filme`: Representa um filme no sistema.
- `Genero`: Representa um gênero de filme.
- `Artista`: Representa um artista.
- `Personagem`: Representa um personagem.
- `FilmeGenero`: Faz a relação muitos-para-muitos entre filmes e gêneros.
- `FilmeArtista`: Faz a relação muitos-para-muitos entre filmes e artistas.
- `Contato`: Representa um contato para Feedback.


### 🎨 Views
- **Filme**: Páginas para listagem, detalhes, criação, edição e exclusão.
- **Genero**: Páginas para listagem, criação, edição e exclusão.
- **Artista**: Páginas para listagem, criação, edição e exclusão.
- **Personagem**: Páginas para listagem, criação, edição e exclusão.
- **Contato**: Páginas para listagem, criação, edição e exclusão.

### 🌐 Controllers
- `FilmesController`: Gerencia as operações relacionadas aos filmes.
- `GenerosController`: Gerencia as operações relacionadas aos gêneros.
- `ArtistasController`: Gerencia as operações relacionadas aos artistas.
- `PersonagensController`: Gerencia as operações relacionadas aos personagens.
- `ContatosController`: Gerencia as operações relacionadas aos contatos.

---

## ⚙️ Como Executar o Projeto

### Pré-requisitos
1. **.NET SDK** (8.0 ou superior)
2. **Banco de Dados** configurado e rodando (SQL Server.)
3. **Visual Studio** ou outro editor de sua preferência.

### Passos
1. Clone o repositório:
   ```bash
   git clone https://github.com/Belinaci/ProjetoDAAW.git
