# Beoordelen opdracht

```mermaid
sequenceDiagram
    Docent->>Systeem: Vraagt alle opdrachten op
    Systeem-->>Docent: Weergeeft alle ingeleverde opdrachten
    Docent->>Systeem: Selecteerd een specifieke opdracht
    Systeem-->>Docent: Weergeeft de opdracht 
    Docent->>Systeem: Vult een beoordeling met feedback in
    Systeem-->>Systeem:  Valideert de gegevens en slaat de data op