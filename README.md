# Intelligent Document Assistant

Proof of Concept to use Azure AI Services to build an Intelligent Document Assistant for a Console application.

The app tries to listen to a directory for file changes, on detecting a new document, if it is a useable/readable identity document, it reads it and indexes relevant content to a local SQL Server Database (as proof of concept).

This codebase uses Azure Speech Service, Azure Document Intelligence, Azure Language Service, to mention a few for the whole logic.

Future work might include the use of Azure Functions, Azure Logic Apps, Azure Data Storage, you know, you might think I am an Azure fan 🙂
