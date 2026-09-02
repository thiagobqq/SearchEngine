# Search Engine

Motor de busca pessoal construído (quase) do zero

1. **WebCrawler** — Coleta páginas da web e salva no banco de dados
2. **ETL Service** — Recebe páginas novas via RabbitMQ, tokeniza, stemmea e monta o inverted index
3. **SearchEngine** — API de busca com TF-IDF e ranking

## Tecnologias

- .NET 9
- Entity Framework Core
- SQL Server
- RabbitMQ
- HtmlAgilityPack
- Porter Stemmer

## TODO

- [x] Criar projeto EtlService
- [x] Implementar RabbitMQ Publisher no WebCrawler
- [x] Implementar TokenizerService
- [x] Implementar StemmerService
- [x] Implementar IndexingService
- [x] Implementar ETL Worker
- [x] Criar tabelas SearchTerms, SearchPostings, IndexedPages
- [ ] Ajustar SearchEngine para ler do banco indexado
- [ ] Implementar SearchService com TF-IDF
- [ ] Implementar SearchController
