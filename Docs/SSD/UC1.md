# Inleveren opdrachten

```mermaid
sequenceDiagram
    Student->>Systeem: Selecteer een in te leveren opdracht
    Systeem-->>Student: Weergeeft opdracht
    Student->>Systeem: Levert opdracht in
    Systeem->>Systeem: Valideert verzoek en slaat de data op 