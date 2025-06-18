import numpy as np
from sklearn.ensemble import GradientBoostingRegressor

planet_type_map = { "Rocky": 0, "Ice": 1, "Gas Giant": 2 }
atmosphere_map = {
    "Nitrogen-Oxygen": 0,
    "Hydrogen-Helium": 1,
    "Carbon Dioxide": 2,
    "No Atmosphere": 3
}

# Dummy training data
X = np.array([
    [1.0, 1.0, 300, 0, 0, 0.9],   # High BioScore
    [3.0, 2.0, 5000, 2, 1, 0.1],  # Low
    [0.8, 0.9, 1000, 1, 2, 0.3],
    [1.2, 1.1, 250, 0, 0, 0.8],
    [2.5, 2.3, 4000, 2, 1, 0.2]
])
y = np.array([0.95, 0.2, 0.4, 0.9, 0.3])

model = GradientBoostingRegressor()
model.fit(X, y)

def predict_bioscore(mass, radius, orbital_period, planet_type, atmosphere, water_likelihood) -> float:
    pt_encoded = planet_type_map.get(planet_type, 0)
    atm_encoded = atmosphere_map.get(atmosphere, 3)

    input_data = np.array([[mass, radius, orbital_period, pt_encoded, atm_encoded, water_likelihood]])
    prediction = model.predict(input_data)[0]
    return round(float(prediction), 3)