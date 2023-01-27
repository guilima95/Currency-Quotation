# Currency Quotation EUR-BRL
# How does it work?
Serveless system that monitors the Euro exchange rate and sends a Telegram alert when the value reaches what is configured (configured option) I keep track of the EUR-BRL exchange rate through Awesome API.

When the rate reaches the value I want (configured), the Azure function of type Timer Trigger running every 1 minute in CronJob (* * * * *) sends by Telegram CurrencyQuotation bot (created by me) the value of the currency.

I need to set three values in the environment variable settings:

# MyChat:
Number that represents your Telegram chat, it is unique and each user has one, in this example I set mine. To get yours, it is simple, just make a request using Telegram.BotAPI.

# Token:
When I created the bot from the Telegram documentation, I got the token to use Lib Telegram.BotAPI.

# EURBRLAlert:
Base value that I want to receive the alert when the Euro arrives.

# Approaches, Technologies ...
Azure Function type isoleted .NET 7 Function
Integration with Telegram Bot using Telegram.BotAPI -> https://github.com/Eptagone/Telegram.BotAPI
Integration with Awesome API using HttpClient to get EUR-BRL Currency Quote values -> https://docs.awesomeapi.com.br/api-de-moedas
Use of default IOptions for settings
