from fastapi import APIRouter
from app.schemas.predict_input import PredictRequest, PredictResponse
from app.models.predictor import predict_type
from app.schemas.predict_input import AtmospherePredictRequest, AtmospherePredictResponse
from app.models.predictor_atmosphere import predict_atmosphere
from app.schemas.predict_input import WaterPredictRequest, WaterPredictResponse
from app.models.predictor_water import predict_water_likelihood
from app.schemas.predict_input import BioScorePredictRequest, BioScorePredictResponse
from app.models.predictor_bioscore import predict_bioscore


router = APIRouter()

@router.post("/predict-type", response_model=PredictResponse)
async def predict(request: PredictRequest):
    prediction = predict_type(request.mass, request.radius, request.orbitalPeriod)
    return PredictResponse(planet_type=prediction)

@router.post("/predict-atmosphere", response_model=AtmospherePredictResponse)
async def predict_atmosphere_route(req: AtmospherePredictRequest):
    result = predict_atmosphere(
        mass=req.mass,
        radius=req.radius,
        orbital_period=req.orbitalPeriod,
        planet_type=req.planetType
    )
    return AtmospherePredictResponse(atmosphere=result)

@router.post("/predict-water", response_model=WaterPredictResponse)
async def predict_water_route(req: WaterPredictRequest):
    result = predict_water_likelihood(
        mass=req.mass,
        radius=req.radius,
        orbital_period=req.orbitalPeriod,
        planet_type=req.planetType,
        atmosphere=req.atmosphere
    )
    return WaterPredictResponse(waterLikelihood=result)

@router.post("/predict-bioscore", response_model=BioScorePredictResponse)
async def predict_bioscore_route(req: BioScorePredictRequest):
    result = predict_bioscore(
        mass=req.mass,
        radius=req.radius,
        orbital_period=req.orbitalPeriod,         
        planet_type=req.planetType,                
        atmosphere=req.atmosphere,
        water_likelihood=req.waterLikelihood       
    )
    return BioScorePredictResponse(bioScore=result)