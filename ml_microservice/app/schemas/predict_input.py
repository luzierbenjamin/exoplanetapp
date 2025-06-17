from pydantic import BaseModel

class PredictRequest(BaseModel):
    mass: float
    radius: float

class PredictResponse(BaseModel):
    planet_type: str
