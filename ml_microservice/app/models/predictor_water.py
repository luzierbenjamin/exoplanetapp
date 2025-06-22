import numpy as np
from sklearn.ensemble import RandomForestRegressor

# Updated training data
X = np.array([
    [1.0, 1.0, 300, 0, 0],  # Rocky, Nitrogen-Oxygen
    [3.0, 2.0, 5000, 2, 1], # Gas Giant, Hydrogen
    [0.8, 0.9, 1000, 1, 2], # Ice, CO2
    [1.2, 1.1, 250, 0, 0],
    [2.5, 2.3, 4000, 2, 1],
    [0.4, 0.4, 50, 0, 3],   # No Atmosphere
    [0.5, 0.5, 100, 1, 3]   # No Atmosphere
])
y = np.array([0.9, 0.1, 0.3, 0.8, 0.2, 0.0, 0.0])

planet_type_map = { "Rocky": 0, "Ice": 1, "Gas Giant": 2 }
atmosphere_label_map = {
    0: "Nitrogen-Oxygen",
    1: "Hydrogen-Helium",
    2: "Carbon Dioxide",
    3: "No Atmosphere"
}
atmosphere_map = {v: k for k, v in atmosphere_label_map.items()}

model = RandomForestRegressor()
model.fit(X, y)

def predict_water_likelihood(mass: float, radius: float, orbital_period: float, planet_type: str, atmosphere: str) -> float:
    pt_encoded = planet_type_map.get(planet_type, 0)
    atm_encoded = atmosphere_map.get(atmosphere, 3)
    input_data = np.array([[mass, radius, orbital_period, pt_encoded, atm_encoded]])
    prediction = model.predict(input_data)[0]
    return round(float(prediction), 3)
