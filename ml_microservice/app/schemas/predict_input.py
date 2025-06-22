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

class AtmospherePredictResponse(BaseModel):
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
    orbital_period: float
    planet_type: str
    atmosphere: str
    water_likelihood: float

class BioScorePredictResponse(BaseModel):
    bioScore: float