from pydantic import BaseModel

class PredictRequest(BaseModel):
    mass: float
    radius: float
    orbitalPeriod: float

class PredictResponse(BaseModel):
    planet_type: str

class AtmospherePredictRequest(BaseModel):
    mass: float
    radius: float
    orbitalPeriod: float
    planetType: str

class AtmospherePredict(BaseModel):
    atmosphere: str

class WaterPredictRequest(BaseModel):
    mass: float
    radius: float
    orbitalPeriod: float
    planetType: str
    atmosphere: str

class WaterPredictResponse(BaseModel):
    waterLikelihood: float

class BioScorePredictRequest(BaseModel):
    mass: float
    radius: float
    orbitalPeriod: float
    planetType: str
    atmosphere: str
    waterLikelihood: float

class BioScorePredictionResponse(BaseModel):
    bioScore: float