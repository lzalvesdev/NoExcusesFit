
# NoExcusesFit API

API REST para gestão esportivas — com autenticação segura e controle de acesso por perfil.

---

## O que tem aqui?

- 🔐 Autenticação com JWT + refresh token (com rotação e revogação)
- 👥 Controle de acesso por roles (Admin, Manager, Coach, User)
- ⚡ Proteção com rate limiting em endpoints sensíveis
- 🛡️ Middleware global de tratamento de erros
- 🧪 Testes unitários (Moq + FluentAssertions)
- 🔄 CI com GitHub Actions

---

## Sobre o projeto

Esse projeto foi criado com a ideia de ir além de um CRUD básico.

Aqui eu foquei em simular um cenário mais real:
- controle de usuários com diferentes permissões
- autenticação mais robusta
- organização clara das responsabilidades

A ideia foi praticar como estruturar uma API pensando em **manutenção, segurança e evolução**.

---

## Tecnologias

- .NET 9
- Dapper (acesso a dados com SQL direto)
- SQL Server
- JWT + Refresh Token
- BCrypt
- Rate Limiting
- xUnit + Moq + FluentAssertions
- GitHub Actions

---

## 🧱 Como está organizado?

Arquitetura em Clean Architecture:

```
NoExcusesFit              — API (controllers, middlewares, filtros)
NoExcusesFit.Domain       — entidades, interfaces, DTOs, exceções
NoExcusesFit.Business     — regras de negócio
NoExcusesFit.Data         — acesso a dados (Dapper / SQL)
NoExcusesFit.Tests        — testes unitários
```

**Regra principal:**
cada camada sabe só o necessário (baixo acoplamento)

---

## Autenticação e Autorização

O sistema utiliza JWT com refresh token rotativo:

- Login retorna **access token (curta duração)** + **refresh token**
- Ao renovar, o refresh token anterior é **revogado**
- Logout invalida o refresh token

**Roles:**

| Role    | Permissão |
|---------|-----------|
| Admin   | Acesso total |
| Manager | Gerencia operação |
| Coach   | Visualiza atletas e especialidades |
| Athlete | Evolução futura |
| User    | Padrão ao registrar |

---

## Endpoints

### Auth
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | /Auth/register | Cria uma conta |
| POST | /Auth/login | Autentica e retorna tokens |
| POST | /Auth/refresh | Renova o access token |
| POST | /Auth/logout | Revoga o refresh token |

### UserAccount — Admin
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /UserAccount | Lista usuários paginado |
| GET | /UserAccount/{id} | Busca por id |
| PUT | /UserAccount/{id} | Atualiza nome e email |
| DELETE | /UserAccount/{id} | Remove usuário |

### Coach — Admin, Manager
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /Coach | Lista coaches paginado |
| GET | /Coach/{id} | Busca por id com atletas e especialidades |
| POST | /Coach | Cria um coach |
| POST | /Coach/{id}/speciality/{specialityId} | Atribui especialidade ao coach |
| DELETE | /Coach/{id} | Remove coach |

### Athlete — Admin, Manager
### Athlete — Coach pode apenas consultar (GET)
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /Athlete | Lista atletas paginado |
| GET | /Athlete/{id} | Busca por id |
| POST | /Athlete | Cria um atleta |
| PUT | /Athlete/{id} | Reatribui atleta a outro coach |
| DELETE | /Athlete/{id} | Remove atleta |

### Speciality — Admin, Manager
### Athlete — Coach pode apenas consultar (GET)
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /Speciality | Lista todas |
| POST | /Speciality | Cria especialidade |
| PUT | /Speciality/{id} | Atualiza descrição |
| DELETE | /Speciality/{id} | Remove especialidade |

---

## Como rodar

1. Clone o repositório
```bash
git clone https://github.com/lzalvesdev/NoExcusesFit.git
```

2. Execute o script de criação do banco disponível em `noexcusesfit.sql`

3. Configure a connection string e as configurações JWT no `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "SqlLocal": "Server=localhost;Database=NoExcusesFit;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "Secret": "sua-chave-secreta-aqui-minimo-32-caracteres",
    "Issuer": "NoExcusesFit",
    "Audience": "NoExcusesFitUsers",
    "ExpirationInMinutes": 15
  }
}
```

4. Rode a aplicação

---

## Testes

```bash
dotnet test
```

Os testes cobrem cenários críticos de autenticação: login inválido, senha incorreta, email duplicado, refresh token revogado e fluxo de sucesso.

---

## CI/CD

Pipeline com GitHub Actions executando build automático e testes a cada push na branch principal.

---

## Próximos passos

- [ ] Docker Compose (API + SQL Server)
- [ ] Aumentar cobertura de testes
- [ ] Observabilidade (logs e métricas)
- [ ] Evolução do perfil do atleta
