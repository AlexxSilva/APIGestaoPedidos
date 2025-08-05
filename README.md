# Projeto: Sistema de Pedidos com Comunicação via RabbitMQ

> ⚠️ Este projeto está em construção.

## 🧱 Visão Geral

Este sistema é composto por dois microserviços:

1. **API de Gestão de Pedidos**
   - Cria e aprova pedidos.
   - Publica eventos de aprovação via RabbitMQ.

2. **API de Pagamento**
   - Consome eventos de aprovação de pedido.
   - Processa o pagamento do pedido.


APIPagamento/ (em breve)

📡 Comunicação entre Serviços
Utiliza RabbitMQ para troca de mensagens.

Evento PedidoAprovadoEvent é publicado pela API de pedidos e consumido pela API de pagamento.

🧪 Tecnologias Utilizadas
.NET 8

ASP.NET Core

RabbitMQ

Entity Framework Core

SQL Server (ou outro banco relacional)

Docker (em breve)
