# Les toevoegen aan vak

```mermaid
sequenceDiagram
    Docent->>Systeem: Vraagt om een nieuwe les toe te voegen aan het vak
    Systeem-->>Docent: Weergeeft alle lessen die toegevoegd kunnen worden
    Docent->>Systeem: VSelecteert een les om toe te voegen
    Systeem-->>Docent: Valideert het verzoek en slaat de wijziging op
   