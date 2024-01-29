# Api Fintech Grupo10

## Introdução

Bem-vindo à documentação da API FintechGrupo10, uma plataforma desenvolvida para oferecer serviços relacionados a investimentos financeiros. 

Este guia visa oferecer uma compreensão abrangente da aplicação, permitindo que os todos possam entender e executar a solução sem dificuldades.

Com uma arquitetura robusta e modular, esta aplicação busca atender às necessidades do setor financeiro, proporcionando soluções eficientes para cadastro de clientes, definição de perfis de investimento, e recomendação personalizada de produtos financeiros.

Este documento tem como objetivo fornecer uma visão detalhada dos requisitos funcionais e não funcionais do projeto. Desde o cadastro de clientes até a sugestão de produtos adaptados a cada perfil de investidor, a API FintechGrupo10 busca oferecer uma experiência completa e personalizada.

Ao longo da documentação, exploraremos os diversos aspectos da aplicação, desde a lógica de cadastro e análise de perfil até a integração com o MongoDB para armazenamento na nuvem, e o uso estratégico do RabbitMQ para comunicação assíncrona entre os diferentes componentes.

Preparamos também uma descrição sucinta de como a autenticação e a segurança são tratadas, assim como a garantia de qualidade por meio do desenvolvimento de testes unitários e visibilidade com os relatórios de cobertura.

Esperamos que este guia seja uma ferramenta útil e valiosa, proporcionando uma visão clara da funcionalidade e da arquitetura da API FintechGrupo10.

## Requisitos Funcionais

### 1. Cadastro de Clientes

A API permite o cadastro de clientes, coletando informações essenciais para a análise de perfil de investimento. No primeiro momento, o cadastro será realizado utilizando as seguintes informações:

- Nome - string
- Documento - string
- Telefone - string
- E-mail - string
- Data de Nascimento - DateTime
- Login - string
- Senha - string
- Permissão  - Enum (roles para a geração do token de acesso, podendo ser 0 para usuário comum ou 1 para usuários administradores)

### 2. Perfil do Cliente

A aplicação define o perfil do cliente com base em uma série de perguntas. Cada pergunta possui três respostas distintas, cada uma com uma pontuação associada. O perfil é determinado pela soma dessas pontuações. 

As perguntas e respostas são implementadas conforme as seguintes diretrizes:

- **Pergunta 1:** Quais investimentos você realizou frequentemente nos últimos 24 meses?
    - Resposta 1: Nunca realizei nenhum tipo de investimento ou apenas em poupança [Pontuação: 3]
    - Resposta 2: Além de poupança, tenho investimentos em CDBs ou fundos de renda fixa [Pontuação: 5]
    - Resposta 3: Além de ter investimentos em poupança, CDBs, fundos de renda fixa, também tenho investimentos em fundos de crédito, e/ou multimercado e/ou de renda variável [Pontuação: 10]
- **Pergunta 2:** Quais os tipos de investimentos que você mais se identifica?
    - Resposta 1: Não me identifico com nenhum investimento [Pontuação: 3]
    - Resposta 2: Poupança e Títulos emitidos com garantia do governo e, eventualmente, fundos de renda fixa [Pontuação: 5]
    - Resposta 3: Além das opções acima, fundos multimercados, fundos de ações e aplicações diretas em ações [Pontuação: 10]
- **Pergunta 3:** Em relação aos seus investimentos, qual é a necessidade futura dos seus rendimentos?
    - Resposta 1: Não tenho previsão para utilização desses recursos [Pontuação: 3]
    - Resposta 2: Preciso desse dinheiro como complemento de renda [Pontuação: 5]
    - Resposta 3: Possibilidade de elevar meu padrão de vida. Eventualmente, posso precisar utilizar uma parte dele [Pontuação: 10]
- **Pergunta 4:** Como você reagiria caso o seu investimento tivesse uma perda de 10%?
    - Resposta 1: Ficaria muito preocupado [Pontuação: 3]
    - Resposta 2: Ficaria apreensivo, mas atento aos próximos resultados, avaliando a possibilidade de reversão [Pontuação: 5]
    - Resposta 3: Avalio o resultado no longo prazo e, nesse caso, no curto prazo, não me preocuparia [Pontuação: 10]
- **Pergunta 5:** O quanto você considera seu conhecimento sobre investimentos?
    - Resposta 1: Não entendo nada, preciso de ajuda [Pontuação: 3]
    - Resposta 2: Conheço algumas coisas básicas, mas não sou expert [Pontuação: 5]
    - Resposta 3: Sei totalmente o que estou fazendo [Pontuação: 10]

Essas perguntas adicionais fornecem uma abordagem mais abrangente na análise do perfil do cliente, contribuindo para recomendações de produtos financeiros mais personalizadas.

A lógica utilizada para definir o perfil do cliente é baseada nas respostas fornecidas. A pontuação total obtida é calculada somando as pontuações associadas a cada resposta fornecida pelo cliente. Essa pontuação total é então utilizada para determinar o perfil do investidor de acordo com os seguintes critérios:

- Se a pontuação total é igual ou inferior a 20, o perfil do investidor é considerado "Conservador".
- Se a pontuação total está entre 20 e 40, o perfil do investidor é considerado "Moderado".
- Se a pontuação total é igual ou superior a 40, o perfil do investidor é considerado "Agressivo".

Essa abordagem categoriza os clientes com base em suas respostas, oferecendo uma classificação que reflete sua propensão a assumir riscos em investimentos. A documentação destaca que essas categorias - Conservador, Moderado e Agressivo - são representativas dos perfis de investidores que variam em aversão ao risco, proporcionando uma visão mais clara das preferências e disposições dos clientes em relação aos investimentos.

### 3. Produtos para Clientes

A API oferece endpoints para cadastrar novos produtos ou retornar os produtos financeiros recomendados com base no perfil do cliente. Esses produtos são adaptados às preferências de investimento, levando em consideração fatores como risco e retorno.

Exemplos dos tipos de produtos que podem ser oferecidos:

1. **Renda Fixa:**
    - **CDB Curto Prazo:** Baixo risco, prazo curto.
    - **Tesouro IPCA+ 2026:** Proteção contra variação de preços, prazo médio.
    - **Fundo DI:** Baixo risco, investe majoritariamente em ativos de renda fixa.
2. **Multimercado:**
    - **Fundo Multimercado Moderado:** Diversificação em diferentes classes de ativos, equilíbrio entre risco e retorno.
3. **Imobiliário:**
    - **LCI Médio Prazo:** Isento de imposto de renda, lastreado em financiamentos imobiliários.
4. **Ações:**
    - **Fundo Ações Setoriais:** Investe em ações de setores específicos, proporcionando diversificação moderada.
    - **Ações Empresas Internacionais:** Investimento direto em ações globais, aproveitando oportunidades internacionais.
5. **Investimentos Inovadores:**
    - **Fundo Startups:** Investe em empresas emergentes, buscando inovação e alto crescimento.
    - **Criptomoedas Diversificadas:** Investimento em diversas criptomoedas, aproveitando a volatilidade do mercado de criptoativos.

Essa abordagem permite uma oferta dinâmica de produtos financeiros, adaptando-se às mudanças no mercado e às preferências dos clientes. Os endpoints específicos da API possibilitam a gestão eficiente desses produtos, oferecendo aos clientes opções alinhadas ao seu perfil de investimento e objetivos financeiros.

## Requisitos Não Funcionais

### 1. Arquitetura em Camadas (DDD)

O projeto segue a arquitetura em camadas, organizada conforme os princípios do DDD (Domain-Driven Design), garantindo uma arquitetura modular e coesa. 

As camadas incluem:

- **Apresentação:** Consumers e Controllers.
- **Aplicação:** Lógica de aplicação e serviços.
- **Domínio:** Modelos de domínio e regras de negócios.
- **Infraestrutura:** Implementação de acesso a dados, integrações e logs.

### 2. Testes Unitários

A aplicação possui uma cobertura abrangente de testes unitários, cobrindo partes críticas do código para garantir robustez e qualidade.

Além disso, a ferramenta ***Report Generator*** foi empregada para criar relatórios detalhados da cobertura de testes. Essa ferramenta oferece um relatório detalhado sobre a extensão da cobertura de testes, indicando áreas específicas do código que foram testadas. Esse relatório proporciona insights valiosos sobre a eficácia dos testes, ajudando a identificar lacunas na cobertura e garantindo uma abordagem mais abrangente.

### 3. Logs

O projeto está configurado para gerar logs, registrando atividades importantes em arquivo. Isso inclui informações sobre operações, erros e eventos relevantes para a aplicação.

### 4. Autenticação do Usuário

A API oferece um sistema para a autenticação de usuários, onde o token do tipo Bearer deve ser incluído nas chamadas dos endpoints para acessar recursos protegidos. Os usuários cadastrados podem se autenticar com base em dois tipos de roles: "user" ou "admin". Essa funcionalidade concede diferentes níveis de permissão, adaptando-se às necessidades específicas de cada usuário.

O token de acesso é obtido por meio do endpoint de Login, onde os usuários fornecem suas credenciais (login e senha). Após autenticação bem-sucedida, o token Bearer é gerado e deve ser incluído no cabeçalho das requisições.

A API valida a autenticação durante cada chamada em endpoint protegidos, verificando a presença e a validade do token Bearer. Se o token não for válido ou expirar, a API retornará um status 401, indicando que o usuário não está autenticado.

Obs.: No momento atual, todos os endpoints que necessitam de autenticação estão aceitando os dois tipos de roles. Uma melhoria para a próxima versão será separar quais endpoints poderão ser utilizados por cada role.

### 5. Banco de Dados

A aplicação utiliza o MongoDB, um banco de dados NoSQL, para armazenar suas informações na nuvem. Essa escolha estratégica proporciona flexibilidade na modelagem de dados, sendo essencial para gerenciar diversas informações sobre os clientes, perfis e produtos financeiros.

A arquitetura escalável do MongoDB garante eficiência e desempenho, enquanto a integração suave entre outros recursos asseguram a segurança e continuidade operacional da aplicação na nuvem. Em resumo, o MongoDB oferece uma solução robusta e adaptável para armazenamento e manipulação eficaz de dados.

### 6. Mensageria

A aplicação faz o uso do RabbitMQ como ferramenta de mensageria, possibilitando uma comunicação eficiente e assíncrona entre os diferentes componentes da aplicação. Essa escolha estratégica oferece escalabilidade e robustez, permitindo o processamento de eventos de maneira distribuída. O RabbitMQ funciona como um intermediário confiável, garantindo a entrega de mensagens.

Com essa arquitetura de mensageria, a aplicação pode lidar com eventos em tempo real e manter um fluxo consistente de comunicação entre os diversos módulos, pois foi desenvolvida já pensando numa possível expansão do serviço, proporcionando uma experiência mais ágil e responsiva para os usuários.

## Conclusão

A API FintechGrupo10 representa o resultado do Tech Challenge da Fase 1 do curso de pós-graduação em Arquitetura de Sistemas .Net com Azure, da FIAP.

Este projeto foi concebido para oferecer uma aplicação sobre investimentos financeiros, desde o cadastro de clientes até a recomendação personalizada de produtos. A documentação aqui apresentada visa facilitar a compreensão e execução da aplicação, proporcionando um guia claro e detalhado.

A arquitetura modular, a integração com serviços na nuvem e a utilização estratégica de tecnologias como MongoDB e RabbitMQ contribuem para a robustez e eficiência da API. Cada seção deste documento foi elaborada para oferecer insights detalhados, abordando desde a lógica de cadastro até os requisitos não funcionais, como autenticação, segurança e a estratégia de testes.

Esperamos que esta documentação seja não apenas um guia eficaz para avaliação, mas também uma fonte valiosa de referência para compreender os aspectos funcionais e técnicos desta aplicação FintechGrupo10. 

Agradecemos a oportunidade de apresentar este projeto e permanecemos à disposição para esclarecer dúvidas ou fornecer informações adicionais conforme necessário.
