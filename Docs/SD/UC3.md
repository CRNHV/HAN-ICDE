# Beoordelen opdracht

```mermaid
sequenceDiagram
    actor User
    User->>Controller: POST /beoordeel
    Controller->>_ingeleverdeOpdrachtService: SlaBeoordelingOp(request)
    _ingeleverdeOpdrachtService->>_ingeleverdeOpdrachtRepository: VoorId(request.InzendingId)
    _ingeleverdeOpdrachtRepository-->>_ingeleverdeOpdrachtService: dbIngeleverdeOpdracht
    alt dbIngeleverdeOpdracht is null or invalid Cijfer
        _ingeleverdeOpdrachtService-->>Controller: false
        Controller-->>User: BadRequest()
    else
        _ingeleverdeOpdrachtService->>OpdrachtBeoordelingRepository: Maak(OpdrachtBeoordeling)        
        _ingeleverdeOpdrachtService-->>Controller: true
        Controller-->>User: Redirect(/docent/opdrachtinzending/{request.InzendingId})
    end