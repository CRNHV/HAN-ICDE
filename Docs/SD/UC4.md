# Inzien beoordeling als student 

```mermaid
sequenceDiagram
    participant Controller as Controller (Index)
    participant Service as _opdrachtBeoordelingService
    participant Repository as _beoordelingRepository
    participant Mapper as _mapper

    activate Controller
    Controller->>Controller: GetUserIdFromClaims()
    alt UserId is null
        Controller-->>Controller: Return BadRequest()        
        deactivate Controller
    else
        activate Controller
        Controller->>Service: HaalBeoordelingenOpVoorUser(userId)
        activate Service
        Service->>Repository: HaalBeoordelingenOpVoorStudent(userId.Value)
        activate Repository
        Repository-->>Service: Beoordelingen (List<OpdrachtBeoordeling>)
        deactivate Repository
        Service->>Mapper: Map<List<OpdrachtMetBeoordelingDto>>(Beoordelingen)
        activate Mapper
        Mapper-->>Service: BeoordelingenDto (List<Dto>)
        deactivate Mapper
        Service-->>Controller: BeoordelingenDto
        deactivate Service
        Controller-->>View: Render Index.cshtml with BeoordelingenDto
        
    end
    deactivate Controller
