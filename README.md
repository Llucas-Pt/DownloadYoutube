# 🎬 Downloader de Vídeo e Música do YouTube

Este projeto permite baixar **vídeos** e **músicas** diretamente do YouTube, oferecendo uma interface simples e fácil de usar.  
A aplicação é dividida em **Front-end (React)** e **Back-end (ASP.NET Core Web API)**, sendo o back-end executado dentro de um **container Docker**, garantindo portabilidade e facilidade na execução.

---

## ⚙️ Como funciona

### 1. Backend rodando no Docker
O back-end contém toda a lógica responsável por processar os downloads.  
Aqui está o processo de inicialização do container:

<img src="https://github.com/Llucas-Pt/DownloadYoutube/raw/main/YoutubeDownload/Front/src/Youtube/GifDocker.gif" width="480"/>

---

### 2. Download via Interface
No front-end, o usuário informa a URL e seleciona o formato (vídeo ou áudio).  
A aplicação envia a requisição para a API, que realiza o download.

<img src="https://github.com/Llucas-Pt/DownloadYoutube/raw/main/YoutubeDownload/Front/src/Youtube/GifDownload.gif" width="480"/>

---

## 🧰 Tecnologias Utilizadas

| Tecnologia | Descrição |
|-----------|-----------|
| **React** | Interface do usuário (Front-end) |
| **C# - ASP.NET Core Web API** | Processamento e lógica do download |
| **Docker** | Container para execução do Back-end |
| **Fetch API** | Comunicação entre Front-end e Back-end |

---

## 🚀 Como executar o projeto 

### **Backend (API)**

1. Acesse a pasta do backend:
```bash
cd YoutubeDownload/Back
````

2. Execute o Docker:

```bash
 docker build -t youtube-backend ./Back
 docker run -p 8080:8080 youtube-backend
```

A API estará disponível em:

```
http://localhost:8080
```

### **Frontend**

1. Acesse a pasta do front:

```bash
cd YoutubeDownload/Front
```

2. Instale as dependências:

```bash
npm install
```

3. Execute:

```bash
npm run dev
```

A aplicação abrirá em:

```
http://localhost:3005
```

---

## 🎯 Objetivos do Projeto

* Praticar integração entre **Front-end e Back-end**
* Utilizar **Docker** para empacotamento e execução do servidor
* Realizar download de mídias na prática
* Criar uma interface funcional e intuitiva

---

## 📄 Licença

Este projeto está sob a licença MIT — sinta-se livre para estudar, modificar e usar como quiser.

---

Feito por **Lucas**

