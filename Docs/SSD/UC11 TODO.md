# Beheren opleiding

```mermaid
sequenceDiagram
    Docent->>Systeem: Kiest opleiding
    Systeem-->>Docent: Toont opleiding
    Docent->>Systeem: Geeft aan een vak te willen toevoegen
    Systeem-->>Docent: Toont lijst van vakken
    Docent->>Systeem: Kiest vak
    Systeem-->>Docent: Koppelt onderdeel aan de opleiding