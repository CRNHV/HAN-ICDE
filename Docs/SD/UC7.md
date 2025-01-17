# Cursus toevoegen aan vak

```mermaid
sequenceDiagram
    participant Controller
    participant VakService as _vakService
    participant VakRepository as _vakRepository
    participant CursusRepository as _cursusRepository

    activate Controller

    Controller->>VakService: KoppelCursus(vakGroupId, cursusGroupId)

    activate VakService

    VakService->>VakRepository: NieuwsteVoorGroepId(vakGroupId)

    activate VakRepository

    VakRepository-->>VakService: dbVak (or null)
    VakService->>CursusRepository: NieuwsteVoorGroepId(cursusGroupId)

    activate CursusRepository

    CursusRepository-->>VakService: cursus (or null)

    deactivate CursusRepository

    VakService->>VakService: Check if dbVak or cursus is null
    alt dbVak or cursus is null
        VakService-->>Controller: false
        Controller-->>Controller: HTTP 400 BadRequest
    else
        VakService->>VakService: Check if cursus is not in dbVak.Cursussen
        alt Cursus not in dbVak.Cursussen
            VakService->>VakRepository: Update(dbVak)
            deactivate VakRepository
        end
        
        VakService-->>Controller: true
        deactivate VakService
        Controller-->>Controller: Redirect to /auteur/vak/get/{vakGroupId}
    end

    
    deactivate Controller
