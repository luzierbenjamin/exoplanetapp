from fastapi import APIRouter
from app.schemas.predict_input import PredictRequest, PredictResponse
from app.models.predictor import predict_type

router = APIRouter()

@router.post("/predict", response_model=PredictResponse)
async def predict(request: PredictRequest):
    prediction = predict_type(request.mass, request.radius)
    return PredictResponse(planet_type=prediction)
