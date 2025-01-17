# Inzien beoordeling als docent 

```mermaid
sequenceDiagram   
    activate DocentController
    DocentController ->> _ingeleverdeService: HaalInzendingDataOp(inzendingId)
    activate _ingeleverdeService
    _ingeleverdeService ->> _ingeleverde_opdrachtRepository: VoorId(inzendingId)
    activate _ingeleverde_opdrachtRepository
    _ingeleverde_opdrachtRepository -->> _ingeleverdeService: inzending
    deactivate _ingeleverde_opdrachtRepository
    alt inzending == null
        _ingeleverdeService -->> DocentController: null
        deactivate _ingeleverdeService
        DocentController -->> User: 404 NotFound
        deactivate DocentController
    else
        activate _ingeleverdeService
         activate DocentController
        _ingeleverdeService ->> _opdrachtRepository: GetByInzendingId(inzendingId)
        activate _opdrachtRepository
        _opdrachtRepository -->> _ingeleverdeService: opdracht
        deactivate _opdrachtRepository
        alt opdracht == null
            _ingeleverdeService -->> DocentController: null
            deactivate _ingeleverdeService
            DocentController -->> User: 404 NotFound
            deactivate DocentController
        else
            activate _ingeleverdeService
            activate DocentController
            _ingeleverdeService ->> Mapper: Map<IngeleverdeOpdrachtDto>(inzending)
            activate Mapper
            _ingeleverdeService ->> Mapper: Map<List<OpdrachtBeoordelingDto>>(inzending.Beoordelingen)
            _ingeleverdeService ->> Mapper: Map<List<BeoordelingCritereaDto>>(opdracht.BeoordelingCritereas)
            Mapper -->> _ingeleverdeService: OpdrachtInzendingDto
            deactivate Mapper
            _ingeleverdeService -->> DocentController: OpdrachtInzendingDto
            deactivate _ingeleverdeService
            DocentController -->> User: Render View (BekijkInzending)
            deactivate DocentController
        end
    end
