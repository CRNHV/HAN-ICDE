# Inzien opdrachten

```mermaid
sequenceDiagram
    participant Controller as BekijkOpdrachtController
    participant OpdrachtService as _opdrachtService
    participant IngeleverdeOpdrachtService as _ingeleverdeOpdrachtService
    participant Repository as _repository
    participant IngeleverdeRepo as _ingeleverdeOpdrachtRepository
    participant Mapper as _mapper

    activate Controller
    Controller->>OpdrachtService: NieuwsteVoorGroepId(opdrachtGroupId)
    activate OpdrachtService
    OpdrachtService->>Repository: NieuwsteVoorGroepId(groupId)
    activate Repository
    Repository-->>OpdrachtService: dbEntity (result)
    deactivate Repository
    OpdrachtService->>Mapper: Map<TReadDto>(dbEntity)
    activate Mapper
    Mapper-->>OpdrachtService: TReadDto (mapped result)
    deactivate Mapper
    OpdrachtService-->>Controller: opdracht (TReadDto)
    deactivate OpdrachtService

    Controller->>IngeleverdeOpdrachtService: HaalInzendingenOp(opdrachtGroupId)
    activate IngeleverdeOpdrachtService
    IngeleverdeOpdrachtService->>IngeleverdeRepo: Lijst(x => x.Opdracht.GroupId == opdrachtId)
    activate IngeleverdeRepo
    IngeleverdeRepo-->>IngeleverdeOpdrachtService: dbInzendingen (List<IngeleverdeOpdracht>)
    deactivate IngeleverdeRepo
    IngeleverdeOpdrachtService->>IngeleverdeOpdrachtService: ConvertAll(dbInzendingen)
    IngeleverdeOpdrachtService-->>Controller: inzendingen (List<IngeleverdeOpdrachtDto)
    deactivate IngeleverdeOpdrachtService

    Controller-->>View: BekijkOpdrachtViewModel (opdracht, inzendingen)
    deactivate Controller
