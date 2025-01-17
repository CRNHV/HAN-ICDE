# Inleveren opdrachten

```mermaid
sequenceDiagram

    OpdrachtController->>ControllerBase: GetUserIdFromClaims();

    activate ControllerBase
    activate OpdrachtController

    ControllerBase-->>OpdrachtController: UserId
    deactivate ControllerBase
    alt UserId is null
        OpdrachtController->>OpdrachtController: return NotFound()
    end

    OpdrachtController->>_ingeleverdeOpdrachtService: LeverOpdrachtIn(userId, opdracht)

    activate _ingeleverdeOpdrachtService

    _ingeleverdeOpdrachtService->>_opdrachtRepository: VoorId(opdracht.OpdrachtId)
    _opdrachtRepository->>_ingeleverdeOpdrachtService: Opdracht? opdracht
    
    alt Opdracht is null      
        _ingeleverdeOpdrachtService->>OpdrachtController: return false
    end

    _ingeleverdeOpdrachtService->>_studentRepository:ZoekStudentNummerVoorUserId(userId)
    activate _studentRepository
    _studentRepository->>_ingeleverdeOpdrachtService: int? studentNummer
    deactivate _studentRepository

    alt studentNummer is null
        _ingeleverdeOpdrachtService->>OpdrachtController: return false
    end

    activate _fileManager
    _ingeleverdeOpdrachtService->>_fileManager: SlaOpdrachtOp(opdracht.Bestand.FileName, opdracht.Bestand)
    _fileManager->>_ingeleverdeOpdrachtService: string bestandsLocatie
    deactivate _fileManager

    alt bestandsLocatie is null or empty: 
        _ingeleverdeOpdrachtService->>OpdrachtController: return false
    end

    _ingeleverdeOpdrachtService->>_IIngeleverdeOpdrachtRepository: Maak(ingeleverdeOpdracht)
    activate _IIngeleverdeOpdrachtRepository
    deactivate _IIngeleverdeOpdrachtRepository
    _ingeleverdeOpdrachtService->>OpdrachtController: return true
    deactivate _ingeleverdeOpdrachtService
    alt result is false:
        OpdrachtController->>OpdrachtController: return BadRequest()
    end

    OpdrachtController->>OpdrachtController: return Redirect()
    deactivate OpdrachtController