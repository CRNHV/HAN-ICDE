# Beheren vakken

```mermaid
sequenceDiagram
    Docent->>Systeem: Stuurt een verzoek om een toets aan te maken
    Systeem-->>Docent: Vraagt om de naam,beschrijving en leeruitkomsten van de toets
    Docent->>Systeem: Geeft de gevraagde informatie op
    Systeem-->>Systeem: Valideert het verzoek en slaat de informatie op.
   